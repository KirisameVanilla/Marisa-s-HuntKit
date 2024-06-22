using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalamud.Interface.Windowing;
using ECommons.Logging;
using ImGuiNET;
using FFXIVClientStructs.FFXIV.Common.Math;

namespace HuntKit.Windows;

public class FindRankA : Window, IDisposable
{
    private Plugin plugin;
    private readonly string[] expansions = { "ShB", "EW" };
    public readonly string[] mapsEW = { "Thavnair", "Garlemald", "Labyrinthos", "Moon", "Elpis", "Ultima Thule" };
    public readonly string[] mapsShB = {"Lakeland"};
    private string[] maps;
    private int selectedExpansion;

    public FindRankA(Plugin plugin):base("FindRankA Window")
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };
        this.plugin = plugin;
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (ImGui.Combo("Version", ref selectedExpansion, expansions, expansions.Length))
        {
            switch (selectedExpansion)
            {
                case 0:
                {
                    PluginLog.Log("maps set to ShB");
                    maps = mapsShB;
                    break;
                }
                case 1:
                {
                    maps = mapsEW;
                    PluginLog.Log("maps set to EW");
                    break;
                }
                default:
                {
                    ImGui.Text(expansions[selectedExpansion]);
                    break;
                }
            }
        }

        ImGui.Text(expansions[selectedExpansion] + ":" + (maps==null ? "null":maps[0]));
    }
}
