using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Domain.Enums
{

    [Flags]
    public enum Permission : int
    {
        // Advertisement permissions
        AdvertisementView = 1 << 0,      // 1
        AdvertisementCreate = 1 << 1,    // 2
        AdvertisementUpdate = 1 << 2,    // 4
        AdvertisementDelete = 1 << 3,    // 8

        // Department permissions
        DepartmentView = 1 << 4,         // 16
        DepartmentCreate = 1 << 5,       // 32
        DepartmentUpdate = 1 << 6,       // 64
        DepartmentDelete = 1 << 7,       // 128

        // Event permissions
        EventView = 1 << 8,              // 256
        EventCreate = 1 << 9,            // 512
        EventUpdate = 1 << 10,           // 1024
        EventDelete = 1 << 11,           // 2048

        // Job permissions
        JobView = 1 << 12,               // 4096
        JobCreate = 1 << 13,            // 8192
        JobUpdate = 1 << 14,            // 16384
        JobDelete = 1 << 15,            // 32768

        // LostItem permissions
        LostItemView = 1 << 16,         // 65536
        LostItemCreate = 1 << 17,       // 131072
        LostItemUpdate = 1 << 18,       // 262144
        LostItemDelete = 1 << 19,       // 524288

        // User permissions
        UserView = 1 << 20,              // 1048576
        UserCreate = 1 << 21,           // 2097152
        UserUpdate = 1 << 22,           // 4194304
        UserDelete = 1 << 23,           // 8388608
    }
}