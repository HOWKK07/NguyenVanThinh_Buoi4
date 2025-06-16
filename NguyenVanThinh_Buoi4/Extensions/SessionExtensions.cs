using System.Text.Json;

namespace NguyenVanThinh_Buoi4.Extensions
{
    public static class SessionExtensions
    {
        /// <summary>
        /// Set giá trị vào key nằm trong session
        /// </summary>
        /// <param name="session">Thông tin session</param>
        /// <param name="key">Khóa</param>
        /// <param name="value">Giá trị: string, int, object, array, ...</param>
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            //JsonSerializer.Serialize : chuyển value thành chuỗi json
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        /// <summary>
        /// Hàm lấy về giá trị của khóa nằm trong session
        /// </summary>
        /// <typeparam name="T">Giá trị</typeparam>
        /// <param name="session">Thông tin session</param>
        /// <param name="key">Khóa</param>
        /// <returns></returns>
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            //JsonSerializer.Deserialize : chuyển giá trị value (chuỗi json) ==> cấu trúc tương ứng
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }

}
