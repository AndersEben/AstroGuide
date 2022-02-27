using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using AstroGuide.Scripts.Planeten;
using AstroGuide.Scripts.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AndroidX.RecyclerView.Widget.RecyclerView;

namespace AstroGuide.Scripts.CustViews
{
    class AddRessourceImage : RecyclerView.Adapter, IItemClickListenener
    {
        List<int> Items;
        Activity Context;

        public AddRessourceImage(Activity context, List<int> items)
        {
            this.Items = items;
            this.Context = context;
        }

        public override int ItemCount
        {
            get { return Items.Count; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ViewHolder vh = holder as ViewHolder;
            vh.Image.SetImageResource(Items[position]);
            var para = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, Einstellungen.TextSizeListOffset / Einstellungen.RecImageSize);
            para.BottomMargin = 90;
            vh.Image.LayoutParameters = para;

            vh.SetItemClickListener(this);
        }

        public void onClick(View itemView, int position, bool isLongClick)
        {
            Console.WriteLine(isLongClick.ToString());
            
            EventHandler<ImageEventArgs> handler = onItemClick;
            handler?.Invoke(this, new ImageEventArgs(itemView, position, isLongClick, Items[position]));
        }

        public void removeView(int position)
        {
            //Karten.RemoveAt(position);
        }

        public void addView(Ressource res)
        {
            //Karten.Add(karte);
        }

        public event EventHandler<ImageEventArgs> onItemClick;

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ressource_image, parent, false);
            ViewHolder vh = new ViewHolder(itemView);

            return vh;
        }

    }
}