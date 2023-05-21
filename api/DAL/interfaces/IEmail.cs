
using System.Threading.Tasks;
using api.DAL.data;

namespace api.DAL.Interfaces
{
    public interface IEmail
    {
        string Send(ModelEmail model);
        string SendTemplate(ModelEmail model);
        string SendPasswordReset(ModelSmallEmail model);
        string SendOperativeReport(ModelOpReport model);
        string SendSER(ModelSER model);

    }
}