﻿/*
*
* 文件名    ：BasicViewModel                             
* 程序说明  : 基础数据
* 更新时间  : 2020-06-07 17：48
* 联系作者  : QQ:779149549 
* 开发者群  : QQ群:874752819
* 邮件联系  : zhouhaogg789@outlook.com
* 视频教程  : https://space.bilibili.com/32497462
* 博客地址  : https://www.cnblogs.com/zh7791/
* 项目地址  : https://github.com/HenJigg/WPF-Xamarin-Blazor-Examples
* 项目说明  : 以上所有代码均属开源免费使用,禁止个人行为出售本项目源代码
*/

namespace Consumption.ViewModel
{
    using Consumption.Core.Common;
    using Consumption.Core.Entity;
    using Consumption.Core.IService;
    using Consumption.Core.Query;
    using Consumption.EFCore.Collections;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 基础数据
    /// </summary>
    public class BasicViewModel : BaseDataViewModel<Basic>
    {
        private readonly IConsumptionService menuService;
        public BasicViewModel()
        {
            menuService = AutofacProvider.Get<IConsumptionService>();
        }

        public override async Task GetPageData(int pageIndex)
        {
            try
            {
                var r = await menuService.GetBasicListAsync(new QueryParameters()
                {
                    PageIndex = pageIndex,
                    PageSize = PageSize,
                    Search = SearchText
                });
                if (r != null && r.success)
                {
                    GridModelList = new ObservableCollection<Basic>();
                    var pagedList = JsonConvert.DeserializeObject<PagedList<Basic>>(r.dynamicObj?.ToString());
                    this.TotalCount = pagedList.TotalCount;
                    pagedList.Items?.ToList().ForEach(arg =>
                    {
                        GridModelList.Add(arg);
                    });
                    base.SetPageCount();
                }
            }
            catch (Exception ex)
            {
                //..
            }
        }
    }
}
