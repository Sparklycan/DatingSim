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

    public int MaxBlockCount { get; private set; }
    public int MinBlockCount { get; private set; }
    private Dictionary<string, int> maxCallCount = new Dictionary<string, int>();
    public int MaxCallCount(string block)
    {
        if (maxCallCount.TryGetValue(block, out int count))
            return count;
        else
            return -1;
    }
    private Dictionary<string, int> minCallCount = new Dictionary<string, int>();
    public int MinCallCount(string block)
    {
        if (minCallCount.TryGetValue(block, out int count))
            return count;
        else
            return -1;
    }
    private Dictionary<string, int> blockCount = new Dictionary<string, int>();
    public int BlockCount(string block)
    {
        if (blockCount.TryGetValue(block, out int count))
            return count;
        else
            return -1;
    }
    private Dictionary<int, int> callCount = new Dictionary<int, int>();
    public int CallCount(int id)
    {
        if (callCount.TryGetValue(id, out int count))
            return count;
        else
            return -1;
    }

    [Serializable]
    private class Call
    {
        public string fromBlock;
        public string toBlock;
        public int commandID;
        public int count;

        public Call(string from, string to, int command)
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

        ids.Clear();
        newCalls.Clear();
        calls.Clear();
        MaxBlockCount = 0;
        MinBlockCount = 0;
        maxCallCount.Clear();
        minCallCount.Clear();
        blockCount.Clear();
        callCount.Clear();
    }

    public Color BlockColor(Fungus.Block block)
    {
        // No data for this block
        if (MaxBlockCount == 0 || !blockCount.ContainsKey(block.BlockName))
            return Color.grey;

        float frac = 1.0f;
        if (MaxBlockCount != MinBlockCount)
            frac = (float)(BlockCount(block.BlockName) - MinBlockCount) / (float)(MaxBlockCount - MinBlockCount);
        return blockGradient.Evaluate(frac);
    }

    public Color ConnectionColor(Fungus.Command command)
    {
        // No data for this connection
        if (!callCount.ContainsKey(command.ItemId))
            return Color.grey;

        int minCount = MinCallCount(command.ParentBlock.BlockName);
        int maxCount = MaxCallCount(command.ParentBlock.BlockName);
        float frac = 1.0f;
        if (minCount != maxCount)
            frac = (float)(CallCount(command.ItemId) - minCount) / (float)(maxCount - minCount);
        return connectionGradient.Evaluate(frac);
    }

    private void OnCall(Command command, Block targetBlock)
    {
        Call call = new Call(command.ParentBlock.BlockName, targetBlock.BlockName, command.ItemId);
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

        // Save collected statistics
        JsonData jsonData = new JsonData();
        jsonData.calls = calls
            .Select(c => c.Value)
            .ToArray();
        jsonData.ids = ids.ToArray();

        string jsonString = JsonUtility.ToJson(jsonData);
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

            foreach (Call call in jsonData.calls)
                AddCalls(call, call.count, false);
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
            foreach (Call call in jsonData.calls)
                AddCalls(call, call.count, false);
        }
    }

    private void AddCalls(Call call, int count, bool newCall)
    {
        int commandID = call.commandID;
        string blockName = call.fromBlock;

        Dictionary<Call, Call> calls = newCall ? newCalls : this.calls;
        if (calls.TryGetValue(call, out Call originalCall))
            originalCall.count += count;
        else
            calls.Add(call, call);

        if (callCount.ContainsKey(commandID))
            callCount[commandID] += count;
        else
            callCount.Add(commandID, count);

        if (blockCount.ContainsKey(blockName))
        {
            blockCount[blockName] += count;
            maxCallCount[blockName] = Mathf.Max(maxCallCount[blockName], callCount[commandID]);
            MaxBlockCount = Mathf.Max(MaxBlockCount, blockCount[blockName]);
        }
        else
        {
            blockCount.Add(blockName, count);
            maxCallCount.Add(blockName, count);
            MaxBlockCount = Mathf.Max(MaxBlockCount, count);
        }

        MinBlockCount = blockCount
            .Select(b => b.Value)
            .Min();
        minCallCount[blockName] = this.newCalls
            .Concat(this.calls)
            .Select(c => c.Key)
            .Where(c => c.fromBlock == blockName)
            .Select(c => c.count)
            .Min();
    }
}