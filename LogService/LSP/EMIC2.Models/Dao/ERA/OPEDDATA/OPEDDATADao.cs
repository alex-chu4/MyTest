using Dapper;
using EMIC2.Models.Dao.Dto.ERA.Model;
using EMIC2.Models.Helper;
using EMIC2.Models.Interface.ERA2.OPEDDATA;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Dao.ERA.OPEDDATA
{
    public class OPEDDATADao : IOPEDDATADao
    {
        public List<T> GetEra2QryMaxList<T>(SearchModelDto data)
            where T : class
        {
            List<T> result = new List<T>();
            StringBuilder sql = new StringBuilder().Append("SELECT * FROM ");

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                switch (data.RPT_CODE)
                {
                    case "A1":
                        sql.Append(" ERA2_QRY_MAX_A1 ");
                        break;
                    case "A2":
                        sql.Append(" ERA2_QRY_MAX_A2 ");
                        break;
                    case "A3":
                        sql.Append(" ERA2_QRY_MAX_A3 ");
                        break;
                    case "A4":
                        sql.Append(" ERA2_QRY_MAX_A4 ");
                        break;
                    case "B1":
                        sql.Append(" ERA2_QRY_MAX_B1 ");
                        break;
                    case "C1":
                        sql.Append(" ERA2_QRY_MAX_C1 ");
                        break;
                    case "C2":
                        sql.Append(" ERA2_QRY_MAX_C2 ");
                        break;
                    case "C3":
                        sql.Append(" ERA2_QRY_MAX_C3 ");
                        break;
                    case "C4":
                        sql.Append(" ERA2_QRY_MAX_C4 ");
                        break;
                    case "D1":
                        sql.Append(" ERA2_QRY_MAX_D1 ");
                        break;
                    case "D2":
                        sql.Append(" ERA2_QRY_MAX_D2 ");
                        break;
                    case "D3":
                        sql.Append(" ERA2_QRY_MAX_D3 ");
                        break;
                    case "D4":
                        sql.Append(" ERA2_QRY_MAX_D4 ");
                        break;
                    case "E1":
                        sql.Append(" ERA2_QRY_MAX_E1 ");
                        break;
                    case "E2":
                        sql.Append(" ERA2_QRY_MAX_E2 ");
                        break;
                    case "E3":
                        sql.Append(" ERA2_QRY_MAX_E3 ");
                        break;
                    case "E4":
                        sql.Append(" ERA2_QRY_MAX_E4 ");
                        break;
                    case "E5":
                        sql.Append(" ERA2_QRY_MAX_E5 ");
                        break;
                    case "E6":
                        sql.Append(" ERA2_QRY_MAX_E6 ");
                        break;
                    case "E7":
                        sql.Append(" ERA2_QRY_MAX_E7 ");
                        break;
                    case "E8":
                        sql.Append(" ERA2_QRY_MAX_E8 ");
                        break;
                    case "E9":
                        sql.Append(" ERA2_QRY_MAX_E9 ");
                        break;
                    case "F6":
                        sql.Append(" ERA2_QRY_MAX_F6 ");
                        break;
                    case "F7":
                        sql.Append(" ERA2_QRY_MAX_F7 ");
                        break;
                    case "F8":
                        sql.Append(" ERA2_QRY_MAX_F8 ");
                        break;
                    case "G1":
                        sql.Append(" ERA2_QRY_MAX_G1 ");
                        break;
                    case "H1":
                        sql.Append(" ERA2_QRY_MAX_H1 ");
                        break;
                    case "J1":
                        sql.Append(" ERA2_QRY_MAX_J1 ");
                        break;
                    case "J2":
                        sql.Append(" ERA2_QRY_MAX_J2 ");
                        break;
                    case "J3":
                        sql.Append(" ERA2_QRY_MAX_J3 ");
                        break;
                    case "J4":
                        sql.Append(" ERA2_QRY_MAX_J4 ");
                        break;
                    case "J5":
                        sql.Append(" ERA2_QRY_MAX_J5 ");
                        break;
                    case "K1":
                        sql.Append(" ERA2_QRY_MAX_K1 ");
                        break;
                }
                sql.Append(" (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ");

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID, //預設 null
                    P_PRJ_NO = data.PRJ_NO, //預設 null
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID, //傳入參數
                };

                result = conn.Query<T>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表 F1
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F1></returns>
        public List<ERA2_QRY_MAX_F1> GetEra2QryMaxListF1(SearchModelDto data)
        {
            List<ERA2_QRY_MAX_F1> result = new List<ERA2_QRY_MAX_F1>();

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder().Append("SELECT * FROM ERA2_QRY_MAX_F1 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ");

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID, //預設 null
                    P_PRJ_NO = data.PRJ_NO, //預設 null
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID, //傳入參數
                };

                result = conn.Query<ERA2_QRY_MAX_F1>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表 F1S
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F1S></returns>
        public List<ERA2_QRY_MAX_F1_S> GetEra2QryMaxListF1S(SearchModelDto data)
        {
            List<ERA2_QRY_MAX_F1_S> result = new List<ERA2_QRY_MAX_F1_S>();

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder().Append("SELECT * FROM ERA2_QRY_MAX_F1_S (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ");

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID, //預設 null
                    P_PRJ_NO = data.PRJ_NO, //預設 null
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID, //傳入參數
                };

                result = conn.Query<ERA2_QRY_MAX_F1_S>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表 F21
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F21></returns>
        public List<ERA2_QRY_MAX_F21> GetEra2QryMaxListF21(SearchModelDto data)
        {
            List<ERA2_QRY_MAX_F21> result = new List<ERA2_QRY_MAX_F21>();

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder().Append("SELECT * FROM ERA2_QRY_MAX_F21 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ");

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID, //預設 null
                    P_PRJ_NO = data.PRJ_NO, //預設 null
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID, //傳入參數
                };

                result = conn.Query<ERA2_QRY_MAX_F21>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表 F22
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F22></returns>
        public List<ERA2_QRY_MAX_F22> GetEra2QryMaxListF22(SearchModelDto data)
        {
            List<ERA2_QRY_MAX_F22> result = new List<ERA2_QRY_MAX_F22>();

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder().Append("SELECT * FROM ERA2_QRY_MAX_F22 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ");

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID, //預設 null
                    P_PRJ_NO = data.PRJ_NO, //預設 null
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID, //傳入參數
                };

                result = conn.Query<ERA2_QRY_MAX_F22>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表 F23
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F23></returns>
        public List<ERA2_QRY_MAX_F23> GetEra2QryMaxListF23(SearchModelDto data)
        {
            List<ERA2_QRY_MAX_F23> result = new List<ERA2_QRY_MAX_F23>();

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder().Append("SELECT * FROM ERA2_QRY_MAX_F23 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ");

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID, //預設 null
                    P_PRJ_NO = data.PRJ_NO, //預設 null
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID, //傳入參數
                };

                result = conn.Query<ERA2_QRY_MAX_F23>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表 F31
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F31></returns>
        public List<ERA2_QRY_MAX_F31> GetEra2QryMaxListF31(SearchModelDto data)
        {
            List<ERA2_QRY_MAX_F31> result = new List<ERA2_QRY_MAX_F31>();

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder().Append("SELECT * FROM ERA2_QRY_MAX_F31 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ");

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID, //預設 null
                    P_PRJ_NO = data.PRJ_NO, //預設 null
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID, //傳入參數
                };

                result = conn.Query<ERA2_QRY_MAX_F31>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表 F32
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F32></returns>
        public List<ERA2_QRY_MAX_F32> GetEra2QryMaxListF32(SearchModelDto data)
        {
            List<ERA2_QRY_MAX_F32> result = new List<ERA2_QRY_MAX_F32>();

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder().Append("SELECT * FROM ERA2_QRY_MAX_F32 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ");

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID, //預設 null
                    P_PRJ_NO = data.PRJ_NO, //預設 null
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID, //傳入參數
                };

                result = conn.Query<ERA2_QRY_MAX_F32>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表 F33
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F33></returns>
        public List<ERA2_QRY_MAX_F33> GetEra2QryMaxListF33(SearchModelDto data)
        {
            List<ERA2_QRY_MAX_F33> result = new List<ERA2_QRY_MAX_F33>();

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder().Append("SELECT * FROM ERA2_QRY_MAX_F33 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ");

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID, //預設 null
                    P_PRJ_NO = data.PRJ_NO, //預設 null
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID, //傳入參數
                };

                result = conn.Query<ERA2_QRY_MAX_F33>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表 F41
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F41></returns>
        public List<ERA2_QRY_MAX_F41> GetEra2QryMaxListF41(SearchModelDto data)
        {
            List<ERA2_QRY_MAX_F41> result = new List<ERA2_QRY_MAX_F41>();

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder().Append("SELECT * FROM ERA2_QRY_MAX_F41 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ");

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID, //預設 null
                    P_PRJ_NO = data.PRJ_NO, //預設 null
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID, //傳入參數
                };

                result = conn.Query<ERA2_QRY_MAX_F41>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表 F42
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F42></returns>
        public List<ERA2_QRY_MAX_F42> GetEra2QryMaxListF42(SearchModelDto data)
        {
            List<ERA2_QRY_MAX_F42> result = new List<ERA2_QRY_MAX_F42>();

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder().Append("SELECT * FROM ERA2_QRY_MAX_F42 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ");

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID, //預設 null
                    P_PRJ_NO = data.PRJ_NO, //預設 null
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID, //傳入參數
                };

                result = conn.Query<ERA2_QRY_MAX_F42>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表 F43
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F43></returns>
        public List<ERA2_QRY_MAX_F43> GetEra2QryMaxListF43(SearchModelDto data)
        {
            List<ERA2_QRY_MAX_F43> result = new List<ERA2_QRY_MAX_F43>();

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder().Append("SELECT * FROM ERA2_QRY_MAX_F43 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ");

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID, //預設 null
                    P_PRJ_NO = data.PRJ_NO, //預設 null
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID, //傳入參數
                };

                result = conn.Query<ERA2_QRY_MAX_F43>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表 F51
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F51></returns>
        public List<ERA2_QRY_MAX_F51> GetEra2QryMaxListF51(SearchModelDto data)
        {
            List<ERA2_QRY_MAX_F51> result = new List<ERA2_QRY_MAX_F51>();

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder().Append("SELECT * FROM ERA2_QRY_MAX_F51 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ");

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID, //預設 null
                    P_PRJ_NO = data.PRJ_NO, //預設 null
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID, //傳入參數
                };

                result = conn.Query<ERA2_QRY_MAX_F51>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表 F52
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F52></returns>
        public List<ERA2_QRY_MAX_F52> GetEra2QryMaxListF52(SearchModelDto data)
        {
            List<ERA2_QRY_MAX_F52> result = new List<ERA2_QRY_MAX_F52>();

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder().Append("SELECT * FROM ERA2_QRY_MAX_F52 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ");

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID, //預設 null
                    P_PRJ_NO = data.PRJ_NO, //預設 null
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID, //傳入參數
                };

                result = conn.Query<ERA2_QRY_MAX_F52>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

        /// <summary>
        /// 查詢通報表 F53
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F53></returns>
        public List<ERA2_QRY_MAX_F53> GetEra2QryMaxListF53(SearchModelDto data)
        {
            List<ERA2_QRY_MAX_F53> result = new List<ERA2_QRY_MAX_F53>();

            using (var conn = new SqlConnection(DBHelper.GetEMIC2DBConnection()))
            {
                StringBuilder sql = new StringBuilder().Append("SELECT * FROM ERA2_QRY_MAX_F53 (@P_EOC_ID, @P_PRJ_NO, @P_RPT_MAIN_ID) ");

                var parameters = new
                {
                    P_EOC_ID = data.EOC_ID, //預設 null
                    P_PRJ_NO = data.PRJ_NO, //預設 null
                    P_RPT_MAIN_ID = data.RPT_MAIN_ID, //傳入參數
                };

                result = conn.Query<ERA2_QRY_MAX_F53>(sql.ToString(), parameters).ToList();

                return result;
            }
        }

    }
}
