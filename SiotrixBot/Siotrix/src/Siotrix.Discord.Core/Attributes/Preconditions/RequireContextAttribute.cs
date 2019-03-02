﻿using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace Siotrix.Discord
{
    [Flags]
    public enum ContextType
    {
        Guild = 0x01,
        DM = 0x02,
        Group = 0x04
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RequireContextAttribute : PreconditionAttribute
    {
        public ContextType Contexts { get; }
        
        public RequireContextAttribute(ContextType contexts)
        {
            Contexts = contexts;
        }

        public override Task<PreconditionResult> CheckPermissions(ICommandContext icontext, CommandInfo command, IServiceProvider map)
        {
            var context = icontext as SocketCommandContext;
            bool isValid = false;

            if ((Contexts & ContextType.Guild) != 0)
                isValid = isValid || context.Channel is IGuildChannel;
            if ((Contexts & ContextType.DM) != 0)
                isValid = isValid || context.Channel is IDMChannel;
            if ((Contexts & ContextType.Group) != 0)
                isValid = isValid || context.Channel is IGroupChannel;

            if (isValid)
                return Task.FromResult(PreconditionResult.FromSuccess());
            else
                return Task.FromResult(PreconditionResult.FromError($"Invalid context for command; accepted contexts: {Contexts}"));
        }
    }
}
