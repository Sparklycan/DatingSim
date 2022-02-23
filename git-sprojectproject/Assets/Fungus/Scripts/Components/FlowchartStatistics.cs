using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Fungus;

[ExecuteInEditMode]
[DisallowMultipleComponent]
[RequireComponent(typeof(Flowchart))]
public class FlowchartStatistics : MonoBehaviour
{

    private Flowchart flowchart;

    public new string name;

    [Space]
    public Gradient blockGradient = new Gradient()
    {
        colorKeys = new GradientColorKey[3] {
            new GradientColorKey(new Color(1.0f, 0.0f, 0.0f), 0.0f),
            new GradientColorKey(new Color(1.0f, 1.0f, 0.0f), 0.5f),
            new GradientColorKey(new Color(0.0f, 1.0f, 0.0f), 1.0f)
            },
        alphaKeys = new GradientAlphaKey[2] {
            new GradientAlphaKey(1.0f, 0.0f),
            new GradientAlphaKey(1.0f, 1.0f)
            }
    };
    public Gradient connectionGradient = new Gradient()
    {
        colorKeys = new GradientColorKey[3] {
            new GradientColorKey(new Color(1.0f, 0.0f, 0.0f), 0.0f),
            new GradientColorKey(new Color(1.0f, 1.0f, 0.0f), 0.5f),
            new GradientColorKey(new Color(0.0f, 1.0f, 0.0f), 1.0f)
            },
        alphaKeys = new GradientAlphaKey[2] {
            new GradientAlphaKey(1.0f, 0.0f),
            new GradientAlphaKey(1.0f, 1.0f)
            }
    };

    // Files & directories
    public string FilePath => Directory.GetCurrentDirectory() + '\\' + Path.GetDirectoryName(UnityEngine.SceneManagement.SceneManager.GetActiveScene().path);
    public string FileName => (name == "" ? "" : (name + '-')) + flowchart.GetName();
    public string StatisticsDirectory => FileName + "-Statistics";
    public string StatisticsPath => FilePath + '\\' + StatisticsDirectory;
    public string FileExtestion => ".fs";
    public string LocalFile => FilePath + '\\' + FileName + FileExtestion;
    public string UniqueFilename(string key) => FileName + '-' + key + FileExtestion;
    public string UniqueFile(string key) => StatisticsPath + '\\' + UniqueFilename(key);

    // Array of commands that are being observed
    private IBlockCaller[] blockCallers = new IBlockCaller[0];
    
    private Dictionary<Call, Call> newCalls = new Dictionary<Call, Call>();
    private Dictionary<Call, Call> calls = new Dictionary<Call, Call>();
    private HashSet<long> ids = new HashSet<long>();

    // The highest & lowest count for calls that lead away from the blocks
    private Dictionary<int, int> maxCallCount = new Dictionary<int, int>();
    public int MaxCallCount(int blockID)
    {
        if (maxCallCount.TryGetValue(blockID, out int count))
            return count;
        else
            return -1;
    }
    private Dictionary<int, int> minCallCount = new Dictionary<int, int>();
    public int MinCallCount(int blockID)
    {
        if (minCallCount.TryGetValue(blockID, out int count))
            return count;
        else
            return -1;
    }
    // The total number off calls that have lead away from the blocks
    private Dictionary<int, int> blockCount = new Dictionary<int, int>();
    public int BlockCount(int blockID)
    {
        if (blockCount.TryGetValue(blockID, out int count))
            return count;
        else
            return -1;
    }
    // The number of times a call have been used
    private Dictionary<int, int> callCount = new Dictionary<int, int>();
    public int CallCount(int callID)
    {
        if (callCount.TryGetValue(callID, out int count))
            return count;
        else
            return -1;
    }

    [Serializable]
    private class Call
    {
        public int fromBlock;
        public int toBlock;
        public int commandID;
        public int count;

        public Call(int from, int to, int command)
        {
            fromBlock = from;
            toBlock = to;
            commandID = command;
            count = 1;
        }

        public override bool Equals(object obj)
        {
            Call other = obj as Call;
            if (other == null)
                return false;

            if (fromBlock != other.fromBlock)
                return false;
            if (toBlock != other.toBlock)
                return false;
            if (commandID != other.commandID)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return commandID;
        }
    }

    [Serializable]
    private class JsonData
    {
        public long[] ids;
        public Call[] calls;
        public BlockStatistics[] blocks;
    }

    [Serializable]
    private class BlockStatistics
    {
        public int blockID;
        public int[] blocks;
        public int[] blockCounts;
        public int[] commands;
        public int[] commandCounts;
    }

    private void OnEnable()
    {
        flowchart = GetComponent<Flowchart>();

        blockCallers = flowchart.GetComponents<IBlockCaller>();

        foreach(IBlockCaller caller in blockCallers)
            caller.onCall += OnCall;

        LoadData(true, true);
    }

    private void OnDisable()
    {
        foreach (IBlockCaller caller in blockCallers)
            caller.onCall -= OnCall;

        // No command is being observed anymore
        blockCallers = new IBlockCaller[0];

        SaveData();
    }

    public void ClearData(bool deleteNewStatistics, bool deleteFile)
    {
        if (deleteNewStatistics && Directory.Exists(StatisticsPath))
        {
            Directory.Delete(StatisticsPath, true);
            if (File.Exists(FilePath + '\\' + StatisticsDirectory + ".meta"))
                File.Delete(FilePath + '\\' + StatisticsDirectory + ".meta");
        }
        if (deleteFile && File.Exists(LocalFile))
            File.Delete(LocalFile);

        Block.ClearVisitedBlocks();
        Block.ClearVisitedCommands();
        foreach (Block block in flowchart.GetComponents<Block>())
        {
            block.ClearBlockStatistics();
            block.ClearCommandtatistics();
        }

        ids.Clear();
        newCalls.Clear();
        calls.Clear();
        maxCallCount.Clear();
        minCallCount.Clear();
        blockCount.Clear();
        callCount.Clear();
    }

    public Color BlockColor(Fungus.Block block)
    {
        Color color;
        if (GetPathBlockColor(block, flowchart.SelectedBlock, out color))
            return color;
        else if (GetBlockColor(block, out color))
            return color;
        else
            return Color.grey;
    }

    private bool GetBlockColor(Block block, out Color color)
    {
        IEnumerable<int> counts = flowchart.GetComponents<Block>()
            .Where(b => b.BlockStatistics.ContainsKey(b))
            .Select(b => b.BlockStatistics[b]);

        // No data for this block
        if (!counts.Any() || !block.BlockStatistics.ContainsKey(block))
        {
            color = Color.gray;
            return false;
        }

        float count = block.BlockStatistics[block];
        float min = counts.Min();
        float max = counts.Max();

        float frac = 1.0f;
        if (max != min)
            frac = (count - min) / (max - min);

        color = blockGradient.Evaluate(frac);
        return true;
    }

    private bool GetPathBlockColor(Block block, Block selectedBlock, out Color color)
    {
        color = Color.gray;
        if (selectedBlock == null)
            return false;

        // No data for this block
        if (!flowchart.SelectedBlock.BlockStatistics.Any(b => b.Key == block))
            return true;

        float count = flowchart.SelectedBlock.BlockStatistics.Single(b => b.Key == block).Value;
        float min = flowchart.SelectedBlock.BlockStatistics.Select(b => b.Value).Min();
        float max = flowchart.SelectedBlock.BlockStatistics.Select(b => b.Value).Max();

        float frac = 1.0f;
        if (max != min)
            frac = (count - min) / (max - min);

        color = blockGradient.Evaluate(frac);
        return true;
    }

    public Color ConnectionColor(Fungus.Command command)
    {
        Color color;
        if (GetPathConnectionColor(command, flowchart.SelectedBlock, out color))
            return color;
        else if (GetConnectionColor(command, out color))
            return color;
        else
            return Color.grey;
    }

    private bool GetConnectionColor(Command command, out Color color)
    {
        // No data for this connection
        if (!callCount.ContainsKey(command.ItemId))
        {
            color = Color.gray;
            return false;
        }

        float minCount = MinCallCount(command.ParentBlock.ItemId);
        float maxCount = MaxCallCount(command.ParentBlock.ItemId);

        float frac = 1.0f;
        if (minCount != maxCount)
            frac = (CallCount(command.ItemId) - minCount) / (maxCount - minCount);

        color = connectionGradient.Evaluate(frac);
        return true;
    }

    private bool GetPathConnectionColor(Command command, Block selectedBlock, out Color color)
    {
        color = Color.gray;
        if (selectedBlock == null)
            return false;

        // No data for this block
        if (!selectedBlock.CommandStatistics.Any(b => b.Key == command))
            return true;

        float count = selectedBlock.CommandStatistics.Single(c => c.Key == command).Value;
        float min = selectedBlock.CommandStatistics.Select(c => c.Value).Min();
        float max = selectedBlock.CommandStatistics.Select(c => c.Value).Max();

        float frac = 1.0f;
        if (max != min)
            frac = (count - min) / (max - min);

        color = connectionGradient.Evaluate(frac);
        return true;
    }

    private void OnCall(Command command, Block targetBlock)
    {
        Block.Visit(command);
        Call call = new Call(command.ParentBlock.ItemId, targetBlock.ItemId, command.ItemId);
        AddCalls(call, 1, true);
    }

    public void SaveData()
    {
        // Generate an ID for this save
        long id = DateTime.Now.Ticks;
        if (newCalls.Count != 0)
            ids.Add(id);

        // Add new data to collected statistics
        foreach (var call in newCalls)
        {
            if (calls.TryGetValue(call.Value, out Call originalCall))
                originalCall.count += call.Value.count;
            else
                calls.Add(call.Value, call.Value);
        }

        foreach(Block block in Block.VisitedBlocks)
        {
            foreach(Block other in Block.VisitedBlocks)
                block.Visit(other, 1);
            foreach(Command command in Block.VisitedCommands)
                block.Visit(command, 1);
        }

        List<BlockStatistics> blockStatistics = new List<BlockStatistics>();
        foreach(Block block in flowchart.GetComponents<Block>())
        {
            if (!block.BlockStatistics.Any())
                continue;

            BlockStatistics statistics = new BlockStatistics();
            statistics.blockID = block.ItemId;
            statistics.blocks = block.BlockStatistics.Select(b => b.Key.ItemId).ToArray();
            statistics.blockCounts = block.BlockStatistics.Select(b => b.Value).ToArray();
            statistics.commands = block.CommandStatistics.Select(c => c.Key.ItemId).ToArray();
            statistics.commandCounts = block.CommandStatistics.Select(c => c.Value).ToArray();
            blockStatistics.Add(statistics);
        }

        // Save collected statistics
        JsonData jsonData = new JsonData();
        jsonData.calls = calls
            .Select(c => c.Value)
            .ToArray();
        jsonData.ids = ids.ToArray();
        jsonData.blocks = blockStatistics.ToArray();

        string jsonString = JsonUtility.ToJson(jsonData);
        Directory.CreateDirectory(Path.GetDirectoryName(LocalFile));
        File.WriteAllText(LocalFile, jsonString);

        if (newCalls.Count == 0)
            return;

        // Save new statistics
        jsonData.calls = newCalls
            .Select(c => c.Value)
            .ToArray();
        jsonData.ids = new long[] { id };

        jsonString = JsonUtility.ToJson(jsonData);
        Directory.CreateDirectory(StatisticsPath);
        File.WriteAllText(UniqueFile(id.ToString()), jsonString);
        newCalls.Clear();
    }

    public void LoadData(bool loadFile, bool loadNew)
    {
        // Load collected statistics
        if (loadFile && File.Exists(LocalFile))
        {
            ClearData(false, false);

            string jsonString = File.ReadAllText(LocalFile);
            JsonData jsonData = JsonUtility.FromJson<JsonData>(jsonString);

            foreach (long id in jsonData.ids)
                ids.Add(id);

            if(jsonData.calls.Length >= 1)
                AddCalls(jsonData.calls[0], jsonData.calls[0].count, true);
            for (int i = 1; i < jsonData.calls.Length; i++)
                AddCalls(jsonData.calls[i], jsonData.calls[i].count, false);

            foreach (BlockStatistics statistics in jsonData.blocks)
            {
                Block block = flowchart.GetComponents<Block>()
                    .SingleOrDefault(b => b.ItemId == statistics.blockID);
                if (block == null)
                    continue;

                for(int i = 0; i < statistics.blocks.Length; i++)
                {
                    Block other = flowchart.GetComponents<Block>()
                        .SingleOrDefault(b => b.ItemId == statistics.blocks[i]);

                    block.Visit(other, statistics.blockCounts[i]);
                }

                for(int i = 0; i < statistics.commands.Length; i++)
                {
                    Command command = flowchart.GetComponents<Command>()
                        .SingleOrDefault(b => b.ItemId == statistics.commands[i]);

                    block.Visit(command, statistics.commandCounts[i]);
                }
            }
        }

        if (!loadNew || !Directory.Exists(StatisticsPath))
            return;

        // Prepare to load new statistics
        string pattern = UniqueFilename("*");
        string[] files = Directory
            .EnumerateFiles(StatisticsPath, pattern)
            .ToArray();
        int pos = (StatisticsPath+pattern).IndexOf('*') + 1;

        foreach (string file in files)
        {
            // Does the file contain new statistics?
            string idString = file.Remove(0, pos);
            idString = idString.Remove(idString.IndexOf('.'));
            long id = long.Parse(idString);
            if (ids.Contains(id))
                continue;

            // Load new statistics
            string jsonString = File.ReadAllText(file);
            JsonData jsonData = JsonUtility.FromJson<JsonData>(jsonString);

            ids.Add(jsonData.ids[0]);
            if (jsonData.calls.Length >= 1)
                AddCalls(jsonData.calls[0], jsonData.calls[0].count, true);
            for (int i = 1; i < jsonData.calls.Length; i++)
                AddCalls(jsonData.calls[i], jsonData.calls[i].count, false);

            foreach (BlockStatistics statistics in jsonData.blocks)
            {
                Block block = flowchart.GetComponents<Block>()
                    .SingleOrDefault(b => b.ItemId == statistics.blockID);
                if (block == null)
                    continue;

                for (int i = 0; i < statistics.blocks.Length; i++)
                {
                    Block other = flowchart.GetComponents<Block>()
                        .SingleOrDefault(b => b.ItemId == statistics.blocks[i]);

                    block.Visit(other, statistics.blockCounts[i]);
                }

                for (int i = 0; i < statistics.commands.Length; i++)
                {
                    Command command = flowchart.GetComponents<Command>()
                        .SingleOrDefault(b => b.ItemId == statistics.commands[i]);

                    block.Visit(command, statistics.commandCounts[i]);
                }
            }
        }
    }

    private void AddCalls(Call call, int count, bool newCall, bool addFromBlock = false)
    {
        int commandID = call.commandID;
        int fromID = call.fromBlock;
        int toID = call.toBlock;

        Dictionary<Call, Call> calls = newCall ? newCalls : this.calls;
        if (calls.TryGetValue(call, out Call originalCall))
            originalCall.count += count;
        else
            calls.Add(call, call);

        if (callCount.ContainsKey(commandID))
            callCount[commandID] += count;
        else
            callCount.Add(commandID, count);

        if (blockCount.ContainsKey(toID))
        {
            blockCount[toID] += count;
        }
        else
        {
            blockCount.Add(toID, count);
        }

        if (addFromBlock)
        {
            if (blockCount.ContainsKey(fromID))
                blockCount[fromID] += count;
            else
                blockCount.Add(fromID, count);
        }

        if (maxCallCount.ContainsKey(fromID))
            maxCallCount[fromID] = Mathf.Max(maxCallCount[fromID], callCount[commandID]);
        else
            maxCallCount.Add(fromID, count);

        minCallCount[fromID] = this.newCalls
            .Concat(this.calls)
            .Select(c => c.Key)
            .Where(c => c.fromBlock == fromID)
            .Select(c => c.count)
            .Min();
    }
}