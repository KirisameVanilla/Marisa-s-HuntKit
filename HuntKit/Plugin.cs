using Dalamud.Game.Command;
using Dalamud.Plugin;
using Dalamud.Interface.Windowing;
using ECommons;
using ECommons.Automation;
using ECommons.DalamudServices;
using HuntKit.Windows;

namespace HuntKit;

public sealed class Plugin : IDalamudPlugin
{
    private const string MainWindowCommand = "/huntkit";
    private const string SetETWindowCommand = "/setet";
    private const string FindRankAWindowCommand = "/ranka";
    private const string shoutET = "/shoutet";

    public static Configuration Configuration { get; set; }
    public readonly WindowSystem WindowSystem = new("HuntKit");
    private ConfigWindow ConfigWindow { get; init; }
    private MainWindow MainWindow { get; init; }
    private FindRankA FindRankA { get; init; }
    private SetET SetET { get; init; }
    private FateHelper FateHelper { get; init; }

    public static void Print(string text)
    {
        Chat.Instance.ExecuteCommand("/e [HuntKit]"+text+"<se.1>");
    }

    public Plugin(DalamudPluginInterface pluginInterface)
    {
        ECommonsMain.Init(pluginInterface, this);

        Configuration = pluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        Configuration.Initialize(pluginInterface);

        ConfigWindow = new ConfigWindow(this);
        MainWindow = new MainWindow(this);
        FindRankA = new FindRankA(this);
        SetET = new SetET();
        FateHelper = new FateHelper();

        WindowSystem.AddWindow(ConfigWindow);
        WindowSystem.AddWindow(MainWindow);
        WindowSystem.AddWindow(FindRankA);
        WindowSystem.AddWindow(SetET);
        WindowSystem.AddWindow(FateHelper);
        
        Svc.Commands.AddHandler(MainWindowCommand, new CommandInfo(OnCommand)
        {
            HelpMessage = "call out main window"
        });

        Svc.Commands.AddHandler(SetETWindowCommand, new CommandInfo(OnCommand)
        {
            HelpMessage = "call out set et window\n" + SetETWindowCommand + " xxxx → set ET to xxxx(at least 3 characters)"
        });

        Svc.Commands.AddHandler(FindRankAWindowCommand, new CommandInfo(OnCommand)
        {
            HelpMessage = "call out find rank A mob window"
        });

        Svc.Commands.AddHandler(shoutET, new CommandInfo(OnCommand)
        {
            HelpMessage = "shout the ET out when the ET is set already"
        });


        pluginInterface.UiBuilder.Draw += DrawUI;

        // This adds a button to the plugin installer entry of this plugin which allows
        // to toggle the display status of the configuration ui
        pluginInterface.UiBuilder.OpenConfigUi += ToggleConfigUI;

        // Adds another button that is doing the same but for the main ui of the plugin
        pluginInterface.UiBuilder.OpenMainUi += ToggleMainUI;
    }

    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();
        ConfigWindow.Dispose();
        MainWindow.Dispose();
        FindRankA.Dispose();
        SetET.Dispose();
        FateHelper.Dispose();

        Svc.Commands.RemoveHandler(MainWindowCommand);
        Svc.Commands.RemoveHandler(SetETWindowCommand);
        Svc.Commands.RemoveHandler(FindRankAWindowCommand);
        Svc.Commands.RemoveHandler(shoutET);
        
        ECommonsMain.Dispose();
    }

    private void OnCommand(string command, string args)
    {
        if (command == MainWindowCommand) ToggleMainUI();
        if (command == SetETWindowCommand)
        {
            if (args.Length == 0)
            {
                ToggleSetET();
            }
            else
            {
                SetET.setET(args);
            }
        }
        if (command == FindRankAWindowCommand) ToggleFindRankA();
        if (command == shoutET) SetET.shoutET();
    }

    private void DrawUI() => WindowSystem.Draw();

    public void ToggleConfigUI() => ConfigWindow.Toggle();
    public void ToggleMainUI() => MainWindow.Toggle();
    public void ToggleFindRankA() => FindRankA.Toggle();
    public void ToggleSetET() => SetET.Toggle();
    public void ToggleFateHelper() => FateHelper.Toggle();
}
