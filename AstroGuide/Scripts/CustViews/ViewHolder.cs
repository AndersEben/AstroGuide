using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroGuide.Scripts.CustViews
{
    public class ViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        private IItemClickListenener itemClickListenener;
        public ImageView Image { get; private set; }

        public ViewHolder(View itemView) : base(itemView)
        {

            Image = itemView.FindViewById<ImageView>(Resource.Id.ResImageHolder);
            //itemView.SetOnClickListener(this);
            //itemView.SetOnLongClickListener(this);
        }

        public void SetItemClickListener(IItemClickListenener clicklistener)
        {
            this.itemClickListenener = clicklistener;
        }

        public void OnClick(View v)
        {
            itemClickListenener.onClick(v, AdapterPosition, false);
        }

        public bool OnLongClick(View v)
        {
            itemClickListenener.onClick(v, AdapterPosition, true);
            return true;
        }
    }
}