using api.DAL.data;

namespace api.DAL.Interfaces
{
      public interface ISMS
    {
         string Send(ModelSMS model);
         string SendProvider2(ModelSmallSMS model);
    
    }
}