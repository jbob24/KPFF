using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KPFF.Web.Model;

namespace KPFF.Web.Models.ViewModels
{
    public class ViewModelBase
    {
        public UserSession UserSession { get; set; }

        public List<KPFF.Entities.Week> Weeks 
        { 
            get
            {
                return KPFF.Business.Week.GetWeeks(KPFF.Business.KPFFCache.Current).Where(w => w.MondayDate >= UserSession.Current.FirstWeek.MondayDate && w.MondayDate <= UserSession.Current.LastWeek.MondayDate).OrderBy(w => w.MondayDate).ToList();
            }
        }
        
    }
}