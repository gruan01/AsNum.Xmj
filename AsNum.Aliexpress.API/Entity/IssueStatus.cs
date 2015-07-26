using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Entity {
    public enum IssueStatus {

        /// <summary>
        /// 买家提起纠纷
        /// </summary>
        WAIT_SELLER_CONFIRM_REFUND,

        /// <summary>
        /// 卖家拒绝纠
        /// </summary>
        SELLER_REFUSE_REFUND,

        /// <summary>
        /// 卖家接受纠纷
        /// </summary>
        ACCEPTISSUE,

        /// <summary>
        /// 等待买家发货
        /// </summary>
        WAIT_BUYER_SEND_GOODS,

        /// <summary>
        /// 买家发货，等待卖家收货
        /// </summary>
        WAIT_SELLER_RECEIVE_GOODS,

        /// <summary>
        /// 仲裁中
        /// </summary>
        ARBITRATING,

        /// <summary>
        /// 卖家响应纠纷超时
        /// </summary>
        SELLER_RESPONSE_ISSUE_TIMEOUT

    }
}
