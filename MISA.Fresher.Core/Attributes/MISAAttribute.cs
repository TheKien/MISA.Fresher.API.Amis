using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Attributes
{
    public class MISAAttribute
    {
        /// <summary>
        /// Atrribute cung cấp cho các property bắt buộc nhập - sử dụng để đánh dấu
        /// </summary>
        /// CreateBy: TTKien(21/12/2021)
        [AttributeUsage(AttributeTargets.Property)]
        public class NotEmpty : Attribute
        {
        }

        /// <summary>
        /// Atrribute để đặt tên cho property - sử dụng để đặt tên
        /// </summary>
        /// CreateBy: TTKien(21/12/2021)
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
        /// Atrribute cung cấp cho các property chỉ dùng để hiển thị - sử dụng để đánh dấu
        /// </summary>
        /// CreateBy: TTKien(21/12/2021)
        [AttributeUsage(AttributeTargets.Property)]
        public class ReadOnly : Attribute
        {
        }

        /// <summary>
        /// Atrribute cung cấp cho các property không được trùng trong database - sử dụng để đánh dấu
        /// </summary>
        /// CreateBy: TTKien(21/12/2021)
        [AttributeUsage(AttributeTargets.Property)]
        public class Unique : Attribute
        {
        }

        /// <summary>
        /// Atrribute cung cấp cho các property không được sửa - sử dụng để đánh dấu
        /// </summary>
        /// CreateBy: TTKien(21/12/2021)
        [AttributeUsage(AttributeTargets.Property)]
        public class NotUpdated : Attribute
        {
        }

        /// <summary>
        /// Atrribute cung cấp cho các property là số điện thoại - sử dụng để đánh dấu
        /// </summary>
        /// CreateBy: TTKien(21/12/2021)
        [AttributeUsage(AttributeTargets.Property)]
        public class PhoneNumber : Attribute
        {
        }

        /// <summary>
        /// Atrribute cung cấp cho các property là email - sử dụng để đánh dấu
        /// </summary>
        /// CreateBy: TTKien(21/12/2021)
        [AttributeUsage(AttributeTargets.Property)]
        public class Email : Attribute
        {
        }

        /// <summary>
        /// Atrribute để đánh dấu nhưng cột trong excel- sử dụng để đặt tên, đánh dấu
        /// </summary>
        /// CreateBy: TTKien(25/12/2021)
        [AttributeUsage(AttributeTargets.Property)]
        public class ExcelColumnName : Attribute
        {
            public string Name;
            public ExcelColumnName(string name)
            {
                this.Name = name;
            }
        }
    }
}
