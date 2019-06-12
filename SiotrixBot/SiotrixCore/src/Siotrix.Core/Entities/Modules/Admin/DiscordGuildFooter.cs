﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Siotrix
{
    public class DiscordGuildFooter : Entity
    {
        public string FooterText { get; set; }
        public string FooterIcon { get; set; }
        public long GuildId { get; set; }
    }
}
