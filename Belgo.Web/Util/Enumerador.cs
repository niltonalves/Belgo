using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

using System.Text;
using System.Reflection;

namespace Belgo.Web.Util
{
    public static class Enumerador
    {
        public enum TipoPergunta
        {
            [Description("Única Escolha")]
            U = 1,
            [Description("Múltipla Escolha")]
            M,
            [Description("Dissertativa")]
            D
        }

        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }
    }
}
