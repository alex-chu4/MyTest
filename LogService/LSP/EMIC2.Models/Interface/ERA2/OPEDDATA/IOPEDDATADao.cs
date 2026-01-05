using EMIC2.Models.Dao.Dto.ERA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.ERA2.OPEDDATA
{
    public interface IOPEDDATADao
    {
        /// <summary>
        /// 查詢通報表 A1 A2 A3 A4 B1 C1 C2 C3 C4 D1 D2 D3 D4 E1 E2 E3 E4 E5 E6 E7 E8 E9 F6 F7 F8 G1 H1 J1 J2 J3 J4 J5 K1
        /// </summary>
        /// <returns> List<T></returns>
        List<T> GetEra2QryMaxList<T>(SearchModelDto data)
            where T : class;

        /// <summary>
        /// 查詢通報表 F1
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F1></returns>
        List<ERA2_QRY_MAX_F1> GetEra2QryMaxListF1(SearchModelDto data);

        /// <summary>
        /// 查詢通報表 F1S
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F1S></returns>
        List<ERA2_QRY_MAX_F1_S> GetEra2QryMaxListF1S(SearchModelDto data);

        /// <summary>
        /// 查詢通報表 F21
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F21></returns>
        List<ERA2_QRY_MAX_F21> GetEra2QryMaxListF21(SearchModelDto data);

        /// <summary>
        /// 查詢通報表 F22
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F22></returns>
        List<ERA2_QRY_MAX_F22> GetEra2QryMaxListF22(SearchModelDto data);

        /// <summary>
        /// 查詢通報表 F23
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F23></returns>
        List<ERA2_QRY_MAX_F23> GetEra2QryMaxListF23(SearchModelDto data);
        
        /// <summary>
        /// 查詢通報表 F31
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F31></returns>
        List<ERA2_QRY_MAX_F31> GetEra2QryMaxListF31(SearchModelDto data);

        /// <summary>
        /// 查詢通報表 F32
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F32></returns>
        List<ERA2_QRY_MAX_F32> GetEra2QryMaxListF32(SearchModelDto data);

        /// <summary>
        /// 查詢通報表 F33
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F33></returns>
        List<ERA2_QRY_MAX_F33> GetEra2QryMaxListF33(SearchModelDto data);

        /// <summary>
        /// 查詢通報表 F41
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F41></returns>
        List<ERA2_QRY_MAX_F41> GetEra2QryMaxListF41(SearchModelDto data);

        /// <summary>
        /// 查詢通報表 F42
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F42></returns>
        List<ERA2_QRY_MAX_F42> GetEra2QryMaxListF42(SearchModelDto data);

        /// <summary>
        /// 查詢通報表 F43
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F43></returns>
        List<ERA2_QRY_MAX_F43> GetEra2QryMaxListF43(SearchModelDto data);

        /// <summary>
        /// 查詢通報表 F51
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F51></returns>
        List<ERA2_QRY_MAX_F51> GetEra2QryMaxListF51(SearchModelDto data);

        /// <summary>
        /// 查詢通報表 F52
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F52></returns>
        List<ERA2_QRY_MAX_F52> GetEra2QryMaxListF52(SearchModelDto data);

        /// <summary>
        /// 查詢通報表 F53
        /// </summary>
        /// <returns> List<ERA2_QRY_MAX_F53></returns>
        List<ERA2_QRY_MAX_F53> GetEra2QryMaxListF53(SearchModelDto data);
    }
}
