using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.MISAAttribute
{
    public class MISAAttribute
    {
        /// <summary>
        /// Atrribute cung cấp cho các property bắt buộc nhập - sử dụng để đánh dấu
        /// CreateBy: TTKien(21/12/2021)
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class NotEmpty:Attribute
        {
        }

        /// <summary>
        /// Atrribute để đặt tên cho property - sử dụng để đặt tên
        /// CreateBy: TTKien(21/12/2021)
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class PropertyName : Attribute
        {
            public string Name;
            public PropertyName(string name)
            {
                this.Name = name;
            }
        }

        /// <summary>
        /// Atrribute cung cấp cho các property chỉ để hiển thị - sử dụng để đánh dấu
        /// CreateBy: TTKien(21/12/2021)
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class ReadOnly : Attribute
        {
        }

        /// <summary>
        /// Atrribute cung cấp cho các property không được trùng nhau - sử dụng để đánh dấu
        /// CreateBy: TTKien(21/12/2021)
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class Unique : Attribute
        {
        }
    }
}
