using TimeFreezesAtMidnight.Helpers;

namespace TimeFreezesAtMidnight.Settings;
internal sealed class ModConfig
{
    internal bool Enabled { get; set; } = true;

    private int _timeFreezesAt = TimeHelper.DefaultValueTimeFreezesAt;
    public int TimeFreezesAt
    {
        get => _timeFreezesAt;
        set => _timeFreezesAt = TimeHelper.ClampTime(value);
    }

    public bool UseOldMethod { get; set; } = false;
}
