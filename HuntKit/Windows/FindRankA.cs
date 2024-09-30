using System;
using Dalamud.Interface.Windowing;
using ECommons.Logging;
using ImGuiNET;
using System.Collections.Generic;
using System.Numerics;
using ECommons.DalamudServices;
using System.Windows.Forms;

namespace HuntKit.Windows;

public class FindRankA : Window, IDisposable
{
    private Plugin plugin;
    private readonly string[] expansions = ["ShB", "EW", "DT"];
    private readonly static string[] MapsEW = ["Thavnair", "Garlemald", "Labyrinthos", "MareLamentorum", "Elpis", "UltimaThule"];
    private readonly static string[] MapsShB = ["Lakeland", "Kholusia", "AmhAraeng", "IlMheg", "Greatwood", "TheTempest"];
    private readonly static string[] MapsDT = ["Urqopacha", "Kozamauka", "YakTel", "Shaaloani", "HeritageFound", "LivingMemory"];
    private readonly List<string[]> expansionMaps = [MapsShB, MapsEW, MapsDT];

    private string[]? maps = null;
    private string[]? selectedMaps = null;
    private string zone = string.Empty;
    private int selectedExpansionIndex = 0, selectedMapIndex = 0;
    private bool isReady = false, isRunning = false;
    private Vector3 playerPos;
    private int index = 0;

    private bool isDebug = false;
    List<Vector3>? waymarks = null;

    public FindRankA(Plugin plugin):base("FindRankA Window")
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
        this.plugin = plugin;
    }

    private double computeDistance(Vector3 a, Vector3 b) => Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2) + Math.Pow((a.Z - b.Z), 2));

    public void Dispose() { }
    private static List<Vector3> FindShortestPath(List<Vector3> points)
    {
        int n = points.Count;
        float[,] distanceMatrix = new float[n, n];

        // Step 1: 计算邻接矩阵
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i != j)
                {
                    distanceMatrix[i, j] = Vector3.Distance(points[i], points[j]);
                }
                else
                {
                    distanceMatrix[i, j] = float.MaxValue; // 自己到自己为无穷大，避免选择自己
                }
            }
        }

        // Step 2: 从起始点开始寻找最短路径
        List<int> visited = new List<int>();
        visited.Add(0); // 起始点已访问

        while (visited.Count < n)
        {
            int lastVisited = visited[visited.Count - 1];
            float minDistance = float.MaxValue;
            int nextPoint = -1;

            // 查找下一个最近的点
            for (int i = 0; i < n; i++)
            {
                if (!visited.Contains(i) && distanceMatrix[lastVisited, i] < minDistance)
                {
                    minDistance = distanceMatrix[lastVisited, i];
                    nextPoint = i;
                }
            }

            visited.Add(nextPoint);
        }

        // Step 3: 根据最短路径顺序重新排序List
        List<Vector3> sortedPoints = new List<Vector3>();
        foreach (var index in visited)
        {
            sortedPoints.Add(points[index]);
        }

        return sortedPoints;
    }

    public override void Draw()
    {
        ImGui.Text("pathfind will become slow in Living Memory. be patient");
        ImGui.Checkbox("Debug##debug", ref isDebug);
        var player = Svc.ClientState.LocalPlayer;
        if (player != null)
        {
            playerPos= player.Position;
        }

        if (isDebug)
        {
            ImGui.Text($"player pos: {playerPos.X} {playerPos.Y} {playerPos.Z}");
            if (ImGui.Button("Clip"))
            {
                Clipboard.SetText($"new Vector3({(int)playerPos.X},{(int)playerPos.Y},{(int)playerPos.Z}),");
            }
        }

        ImGui.Text("This function need vnavmesh to run.\nPlease ensure you have installed vnavmesh before using this function.");

        if (ImGui.Combo("Version", ref selectedExpansionIndex, expansions, expansions.Length))
        {
            selectedMaps = expansionMaps[selectedExpansionIndex];
        }

        if (ImGui.Button("Ensure")) maps = selectedMaps;

        if (maps!=null)
        {
            ImGui.Combo("Zone", ref selectedMapIndex, maps, maps.Length);
            zone = maps[selectedMapIndex];
            if (ImGui.Button("Start"))
            {
                index = 1;
                isReady = true;
                isRunning = true;
                var spawnPoints = Spawnpoints.Spawnpoints.SpawnpointsDictionary[zone];
                waymarks = [playerPos, .. spawnPoints];
                waymarks = FindShortestPath(waymarks);
            }
        }

        if (!NavmeshIPC.PathIsRunning() && isReady && waymarks!=null && isRunning && index< waymarks.Count)
        {
            isReady = false;
            NavmeshIPC.PathfindAndMoveTo(waymarks[index], true);
        }

        if (!isReady && waymarks != null && isRunning && index< waymarks.Count)
        {
            if (computeDistance(waymarks[index], playerPos) < 1)
            {
                isReady=true;
                index++;
            }
        }

        if(waymarks != null&&index == waymarks.Count) isRunning = false;
        if (ImGui.Button("Stop"))
        {
            NavmeshIPC.PathStop();
            isRunning = false;
        }
    }
}
