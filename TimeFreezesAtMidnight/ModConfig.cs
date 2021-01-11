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

        public const int DefaultValueTimeFreezesAt = 2400;

        public const int MinTimeFreezesAt = 0700;

        public const int MaxTimeFreezesAt = 2540;

        private int _timeFreezesAt = DefaultValueTimeFreezesAt;
        public int TimeFreezesAt
        {
            get => _timeFreezesAt;
            set
            {
                var val = value;

                if (val < MinTimeFreezesAt)
                    val = MinTimeFreezesAt;

                if (val > MaxTimeFreezesAt)
                    val = MaxTimeFreezesAt;

                _timeFreezesAt = val - (val % 10);
            }
        }
    }
}
