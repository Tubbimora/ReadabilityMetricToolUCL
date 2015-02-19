// Guids.cs
// MUST match guids.h
using System;

namespace UCL.VSPackageToolbar
{
    static class GuidList
    {
        public const string guidVSPackageToolbarPkgString = "3bac9c0b-e177-4f4a-8067-9057b785f3f4";
        public const string guidVSPackageToolbarCmdSetString = "ec011292-7fa4-4c39-b3d6-a41c3bbb3d93";

        public static readonly Guid guidVSPackageToolbarCmdSet = new Guid(guidVSPackageToolbarCmdSetString);
    };
}