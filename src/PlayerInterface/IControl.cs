using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayerInterface
{
    public interface IControl
    {
        // 这个接口用于实现和AI代码的对接，<del>目前考虑使用MEF方式连接</del>，由于.NET3.5不支持MEF，改为采用反射机制连接
        void Update(IEntity entity);
        string GetTeamName();
    }
}
