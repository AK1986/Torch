﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Torch.UnitTests
{
    public class TestUtils
    {
        public static void CleanupCode(Action action)
        {
            try
            {
                action();
            }
            catch
            {
               
            }
        }
    }
}
