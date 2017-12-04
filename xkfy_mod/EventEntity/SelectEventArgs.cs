using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xkfy_mod.EventEntity
{
    public class SelectEventArgs : EventArgs
    { 
        public readonly string TableName;
        public readonly string[] DataRow;
        public readonly TextBox TextBoxId;
        public readonly TextBox TextBoxName;
        public readonly string SelType;
        public SelectEventArgs(string tbName, string[] row, TextBox txtId, TextBox txtName, string selType)
        {
            this.TableName = tbName;
            this.DataRow = row;
            this.TextBoxId = txtId;
            this.TextBoxName = txtName;
            this.SelType = selType;
        }
    }

    public class SelectButton
    {
        //声明一个处理银行交易的委托
        public delegate void ProcessTranEventHandler(object sender, SelectEventArgs e);
        //声明一个事件
        public event ProcessTranEventHandler ProcessTran;

        protected virtual void OnProcessTran(SelectEventArgs e)
        {
            if (ProcessTran != null)
            {
                ProcessTran(this, e);
            }
        }

        public void Prcess(SelectEventArgs e)
        {
            OnProcessTran(e);
        }
    }
}
