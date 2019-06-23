/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：PagerInfo
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：5/9/2018 2:55:11 PM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PagerInfo
    {
        public string URL { get; set; }

        //第几页 从1开始
        public int PageIndex { get; set; }

        //每页多少条
        public int PageSize { get; set; }

        //总条数
        public int SumCount { get; set; }

        //总页数
        public int PageCount
        {
            get {
                int result = 0;
                if (PageSize > 0 && SumCount > 0)
                {
                    if ((SumCount % PageSize) > 0)
                    {
                        result = (int)(SumCount / PageSize) + 1;
                    }
                    else
                    {
                        result = (int)(SumCount / PageSize);
                    }
                }

                return result;
            
            }
        }


        const int SHOW_COUNT = 6; //这个常量表示两边都有省略号时，中间的分页链接的个数,如 1..4 5 6 7 8 9 .. 100
        private List<int> CalcPages()
        {
            List<int> pages = new List<int>();

            int start = (PageIndex - 1) / SHOW_COUNT * SHOW_COUNT + 1;
            int end = Math.Min(PageCount, start + SHOW_COUNT - 1);

            if (start == 1)
            {
                end = Math.Min(PageCount, end + 1);
            }
            else if (end == PageCount)
            {
                start = Math.Max(1, end - SHOW_COUNT);
            }
            pages.AddRange(Enumerable.Range(start, end - start + 1));

            if (start == PageIndex && start > 2)
            {
                pages.Insert(0, start - 1);
                pages.RemoveAt(pages.Count - 1);//保持显示页号个数统一
            }
            if (end == PageIndex && end + 1 < PageCount)
            {
                pages.Add(end + 1);
                pages.RemoveAt(0);  //保持显示页号个数统一
            }
            if (PageCount > 1)
            {
                if (pages[0] > 1)
                {
                    pages.Insert(0, 1); //如果页列表第一项不是第一页，则在队头添加第一页
                }
                if (pages[1] == 3)
                {
                    pages.Insert(1, 2); //如果是1..3这种情况，则把..换成2
                }
                else if (pages[1] > 3)
                {
                    pages.Insert(1, -1); //插入左侧省略号
                }

                if (pages.Last() == PageCount - 2)
                {
                    pages.Add(PageCount - 1); //如果是 98..100这种情况，则..换成99
                }
                else if (pages.Last() < PageCount - 2)
                {
                    pages.Add(-1); //插入右侧省略号
                }

                if (pages.Last() < PageCount)
                {
                    pages.Add(PageCount); //最后一页
                }
            }

            return pages;
        }

        /// <summary>
        /// 用于显示的页号数组，如果中间有小于0的页号，则表示是省略号
        /// </summary>
        public List<int> Pages
        {
            get
            {
                return CalcPages();
            }
        }
    }
}
