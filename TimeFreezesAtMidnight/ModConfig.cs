using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeFreezesAtMidnight
{
    public class ModConfig
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
}
