﻿using AsNum.WPF.Controls;
using AsNum.Xmj.AliSync;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Entity;
using System;

namespace AsNum.Xmj.OrderManager.ViewModels {
    //[Export(typeof(IOrderDealSubView)), PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class WriteMsgViewModel : VMScreenBase, IOrderDealSubView {
        public override string Title {
            get {
                return "留言";
            }
        }

        public string Ctx {
            get;
            set;
        }

        public bool IsBusy {
            get;
            set;
        }

        private Order Order;
        public void Build(Order order) {
            this.Order = order;
        }


        public Action<string, string> OnSuccess;

        public void SendOrderMessage() {
            this.IsBusy = true;
            this.NotifyOfPropertyChange(() => this.IsBusy);
            DispatcherHelper.DoEvents();

            MessageSync.WriteOrderMessage(this.Order.Account, this.Order.OrderNO, this.Ctx);

            this.IsBusy = false;
            this.NotifyOfPropertyChange(() => this.IsBusy);

            if (this.OnSuccess != null)
                this.OnSuccess(this.Order.OrderNO, this.Order.Account);
        }

        public void SendMessage() {
            this.IsBusy = true;
            this.NotifyOfPropertyChange(() => this.IsBusy);
            DispatcherHelper.DoEvents();
            MessageSync.SendMessage(this.Order.Account, this.Order.BuyerID, this.Ctx);

            this.IsBusy = false;
            this.NotifyOfPropertyChange(() => this.IsBusy);

            if (this.OnSuccess != null)
                this.OnSuccess(this.Order.OrderNO, this.Order.Account);
        }
    }
}