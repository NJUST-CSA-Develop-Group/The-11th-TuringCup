using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayerInterface
{
    /// <summary>
    /// 本处是选手书写代码的接口
    /// </summary>
    public interface IControl
    {
        // 这个接口用于实现和AI代码的对接，<del>目前考虑使用MEF方式连接</del>，由于.NET3.5不支持MEF，改为采用反射机制连接
        /// <summary>
        /// 每帧的更新调用
        /// </summary>
        /// <param name="entity">对操纵角色的控制，以及对信息的获取</param>
        void Update(IEntity entity);

        /// <summary>
        /// 提供队伍名称
        /// </summary>
        /// <returns>队伍名称</returns>
        string GetTeamName();
    }
}
