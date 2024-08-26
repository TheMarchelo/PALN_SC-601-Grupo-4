using System.Collections.Generic;

namespace APS.Web.Models
{
    public class AprobacionViewModel
    {
        public List<CriterioViewModel> Criterios { get; set; } = new List<CriterioViewModel>();
    }

    public class CriterioViewModel
    {
        public string NombreCriterio { get; set; }
        public bool Cumple { get; set; }
    }
}
