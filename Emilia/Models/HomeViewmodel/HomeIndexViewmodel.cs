using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace Emilia.Models.HomeViewmodel
{
    public class IndexViewmodel 
    {
        public List<ShopItem> ShopItems {get; set;}
        public List<SellerItem> SellerItems {get; set;}
    }


    public class ShopItem 
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Image {get; set;}
        public decimal Price {get; set;}
    }

    public class SellerItem 
    {
        public int Id {get; set;}
        public string Name {get;set;}
        public string Logo {get; set;}
    }

    public class PagingList<T> : List<T>
    {
        public int TotalPage {get; private set;}
        public int PageIndex {get; set;}

        public PagingList(List<T> items, int count, int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.TotalPage = (int) Math.Ceiling(count / (double) pageSize);

            this.AddRange(items);
        }

        public bool HasPrev
        {
            get {return PageIndex > 1; }
        }

        public bool HasNext
        {
            get {return PageIndex < TotalPage;}
        }

        public static async Task<PagingList<T>> CreateAsyn(IQueryable<T> query, int pageIndex, int pageSize)
        {
            var count = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagingList<T>(items,count, pageIndex, pageSize);
        }
    }
}