using Microsoft.EntityFrameworkCore;
using SolicitudesAPI.Models;

namespace SolicitudesAPI.Helpers
{

    public static class AppHelper
    {
        public static readonly string OnBehalfOfEmail = "onbehalfof@peakr.app";

        //Email Templates:
        public static long ResetPasswordTemplate = 28137295;
        public static long Verifyemail= 28137299;
        public static long FirstRequestCreated= 28138900;
        public static long FirstQuoteRecieved= 28138901;
        public static long FirstPurchaseOrderCreated= 28139538;
        public static long NewRequestRecieved= 28139288;
        public static long NewWonRequest= 28139290;


        public static readonly string welcomeMsg = "welcome.json";
        public static readonly string firstRequestWon = "message1.txt";
        //public readonly string welcomeMsg = "welcome.json";
        //public readonly string welcomeMsg = "welcome.json";
        //public readonly string welcomeMsg = "welcome.json";
        //public readonly string welcomeMsg = "welcome.json";


    }

    public static class Extensions
    {
        /// <summary>
        /// Returns statusId of workflow based on StatusCode
        /// </summary>
        /// <param name="quote"></param>
        /// <returns></returns>
        public static int GetWorkFlowStatusId(this DbSet<WorkflowStatus> wfStatusList, WorkflowStatusCode wfStatusStep)
        {
            var wfStatus = wfStatusList.FirstOrDefault(x => x.StatusCode == (int) wfStatusStep);
            return wfStatus.StatusId;
        }

        public static WorkflowStatus GetWorkFlowStatus(this DbSet<WorkflowStatus> wfStatusList, WorkflowStatusCode wfStatusStep)
        {
            var wfStatus = wfStatusList.FirstOrDefault(x => x.StatusCode == (int)wfStatusStep);
            return wfStatus;
        }
    }


    public enum WorkflowStatusCode
    {
      PorCotizar =1,
      PorAdjudicar =2,
      PorConfirmar = 3,
      PorEnviar =4,
      EnCamino = 5,
      Entregado = 6,
      Recibido = 7,
      Cerrada = 8,
      Vencida = 9,

    }

    public enum CompanyType
    {
        Buyer = 0,
        Seller = 1,
    }

    public enum QuoteState
    {
        Active = 0,  // this quote is active and compiting
        Expired = 1, //this quote expired
        Adjudicated = 2, //this quote was the winner
        Deleted = 3, // this quote was deleted
        Closed = 4, //another quote was the winner

    }
}

