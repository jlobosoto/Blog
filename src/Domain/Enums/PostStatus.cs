using System.ComponentModel;

namespace Blog.Domain.Enums;
public enum PostStatus
{
    [Description("Pending Approval")]
    PendingApproval = 0,
    [Description("Approved")]
    Approved = 1,
    [Description("Rejected")]
    Rejected = 2
}
