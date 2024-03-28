using TimeFreezesAtMidnight.Helpers;

namespace TimeFreezesAtMidnight.Settings;
public sealed class ModConfig
{
    public bool Enabled { get; set; } = true;

    private int _timeFreezesAt = TimeHelper.DefaultValueTimeFreezesAt;
    public int TimeFreezesAt
    {
        get => _timeFreezesAt;
        set => _timeFreezesAt = TimeHelper.ClampTime(value);
    }

    public bool UseOldMethod { get; set; } = false;
}
