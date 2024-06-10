using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyApp.API.DataModel;

[Table("AccountInfo")]
public class AccountInfo
{

    [Key]
    public int AccountId { get; set; }

    public string Name { get; set; }

    public string Account { get; set; }

    public string Pwd { get; set; }


}
