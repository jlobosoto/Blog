using System.ComponentModel;

namespace Blog.Domain.Enums;

public enum Roles
{
    [Description("Public")]
    Public=1,
    [Description("Writer")]
    Writer =2,
    [Description("Editor")]
    Editor =3
    }

