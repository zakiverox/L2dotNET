﻿using L2dotNET.tables;

namespace L2dotNET.model.npcs.decor
{
    public sealed class L2Chair : L2StaticObject
    {
        public L2Chair()
        {
            ObjId = IdFactory.Instance.NextId();
            Closed = 0;
            MaxHp = 0;
            CurHp = 0;
        }

        public bool IsUsedAlready = false;

        public override string AsString()
        {
            return $"L2Chair:{ObjId} {StaticId} {ClanID}";
        }
    }
}