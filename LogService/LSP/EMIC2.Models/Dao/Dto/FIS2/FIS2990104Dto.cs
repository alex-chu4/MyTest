using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.Dto.FIS2
{
    public class FIS2990104Dto
    {
        public string Start_Time { get; set; }

        public string End_Time { get; set; }

        public string Eoc_ID { get; set; }

        public decimal Prj_No { get; set; }

        public List<QryEvaRows> QryEvaRows { get; set; }

        public List<QryShRows> QryShRows { get; set; }

        public List<QryMaxPeopleMissingRows> QryMaxPeopleMissingRows { get; set; }
    }

    public class QryEvaRows
    {
        public string EVACUATE_AREA_ID { get; set; }

        public string PRJ_NO { get; set; }

        public string RESIDENT_NAME { get; set; }

        public string SEX_TYPE { get; set; }

        public string COUNTRY_NAME { get; set; }

        public string BIRTH_YEAR { get; set; }

        public string IDN { get; set; }

        public string TELEPHONE { get; set; }

        public string STATUS_NAME { get; set; }

        public string SH_NAME { get; set; }

        public DateTime? EVACUATE_TIME { get; set; }

        public string CITY_NAME { get; set; }

        public string TOWN_NAME { get; set; }

        public string NEIGHBOR { get; set; }

        public string RESIDENT_ADDR { get; set; }
    }

    public class QryShRows
    {
        public string SHELTER_ID { get; set; }

        public string PRJ_NO { get; set; }

        public string RESIDENT_NAME { get; set; }

        public string SEX_TYPE { get; set; }

        public string COUNTRY_ID { get; set; }

        public string BIRTH_YEAR { get; set; }

        public string IDN { get; set; }

        public string TELEPHONE { get; set; }

        public string CHECKIN_NAME { get; set; }

        public DateTime? CHECKIN_TIME { get; set; }

        public DateTime? CHECKOUT_TIME { get; set; }

        public string CHECKOUT_NAME { get; set; }

        public string NEIGHBOR { get; set; }

        public string RESIDENT_ADDR { get; set; }

        public string SHELTER_ADDR { get; set; }

        public string CITY_ID { get; set; }

        public string CITY_NAME { get; set; }

        public string TOWN_ID { get; set; }

        public string TOWN_NAME { get; set; }

        public string VILLAGE_ID { get; set; }

        public string VILLAGE_NAME { get; set; }

        public string SHELTER_CODE { get; set; }

        public string SHELTER_NAME { get; set; }

        public string CONTACT_PHONE { get; set; }

        public string CONTACT_MOBILE { get; set; }
    }

    public class QryMaxPeopleMissingRows
    {
        public int SEQ_ID { get; set; }

        public int DISP_DETAIL_ID { get; set; }

        public string CITY_NAME { get; set; }

        public string TOWN_NAME { get; set; }

        public DateTime? HAPPENTIME { get; set; }

        public string HAPPENSPACE { get; set; }

        public string CONTACT_NAME { get; set; }

        public string SEX { get; set; }

        public string AGE { get; set; }

        public string STATUS { get; set; }

        public string REASON { get; set; }

        public string HOSPITAL { get; set; }

        public string SHOW_ORDER { get; set; }
    }
}
