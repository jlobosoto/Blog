using System.ComponentModel;

namespace Blog.Domain.Enums;
public enum CommentType
{
    [Description("Public Comment")]
    ByPublic =0,
    [Description("Writer Comment")]
    ByWriter =1,
    [Description("Editor Comment")]
    ByEditor =2,
    [Description("Editor Reject Comment")]
    ByEditorRejected = 3


}

