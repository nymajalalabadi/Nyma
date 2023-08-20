using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyma.Domain.Models
{
    public class Portfolio
    {
        #region properties

        [Key]
        public long Id { get; set; }

        public long PortfolioCategoryId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "تاریخ شروع")]
        [MaxLength(1000, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Link { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Image { get; set; }

        [Display(Name = "توضیح تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string ImageAlt { get; set; }

        [Display(Name = "الویت")]
        public int Order { get; set; } = 0;

        #endregion

        #region relations

        public PortfolioCategory PortfolioCategory { get; set; }

        #endregion

    }
}
