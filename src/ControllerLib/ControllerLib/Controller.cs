// Copyright (c) CSA, NJUST. All rights reserved.
// Licensed under the Mozilla license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayerInterface;

namespace ControllerLib
{
    class Controller : PlayerInterface.IControl
    {
        public string GetTeamName()
        {
            return "Example Team Name";
        }

        public void Update(IEntity entity)
        {
            // TODO: 逻辑写入位置
            return;
        }
    }
}
