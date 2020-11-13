using ErrorCodeLib.Models;

namespace ErrorCodeLib
{
    public class ErrorCode
    {
        internal static object createArray(ErrorCodeModel code, object data, string lang)
        {
            var message = code.m_cht;
            if (lang == "cht")
            {
                message = code.m_cht;
            } else if (lang == "en")
            {
                message = code.m;
            }

            var NoDataValue = new ErrorCodeNoDataModel { r = code.r, m = message };
            var DataValue = new ErrorCodeDataModel { r = code.r, m = message, d = data };

            if (data != null)
            {
                return DataValue;
            }
            return NoDataValue;
        }

        // ============================================SUCCESS============================================
        public static ResultModel SUCCESS(object body,string msg, string Lang)
        {
            var code = new ErrorCodeModel();
            if (string.IsNullOrEmpty(msg))
                 code = new ErrorCodeModel { r = 0, m = "Success", m_cht = "成功" };
            else
                code = new ErrorCodeModel { r = 0, m = msg, m_cht = msg };
            ResultModel result = new ResultModel { Status = 200 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        // ============================================DATABASE============================================
        public static ResultModel DB_ERROR_GET(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 16, m = "Error while getting data from the database", m_cht = "資料取得錯誤" };
            ResultModel result = new ResultModel { Status = 500 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel DB_ERROR_CREATE(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 17, m = "Error while posting data to the database", m_cht = "資料新增錯誤" };
            ResultModel result = new ResultModel { Status = 500 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel DB_ERROR_UPDATE(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 18, m = "Error while putting data to the database", m_cht = "資料修改錯誤" };
            ResultModel result = new ResultModel { Status = 500 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel DB_ERROR_NOT_FOUND(object body,string field, string Lang)
        {
            var code = new ErrorCodeModel();
            if (string.IsNullOrEmpty(field))
             code = new ErrorCodeModel { r = 20, m = "Record expected but nothing found on database", m_cht = "預期紀錄未在資料庫找到" };
            else
             code = new ErrorCodeModel { r = 20, m = field+" record expected but nothing found on database", m_cht = field+"預期紀錄未在資料庫找到" };
            ResultModel result = new ResultModel { Status = 404 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel DB_ERROR_WRONG_STATUS(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 24, m = "Record not active, disabled or unknown status", m_cht = "紀錄未啟用、無效或未支的狀態" };
            ResultModel result = new ResultModel { Status = 500 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        // ============================================PARAMETERS============================================
        public static ResultModel PARAMETER_REQUIRED(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 32, m = "Error while processing required parameter", m_cht = "處理必要參數時錯誤" };
            ResultModel result = new ResultModel { Status = 422 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel PARAMETER_JSON_FORMAT(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 33, m = "Error while processing json formatted parameter", m_cht = "處理Json格式的參數時錯誤" };
            ResultModel result = new ResultModel { Status = 422 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel PARAMETER_EMAIL_FORMAT(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 34, m = "Wrong email format", m_cht = "錯誤的電子信箱格式" };
            ResultModel result = new ResultModel { Status = 422 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel PARAMETER_UNSUPPORTED_FORMAT(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 36, m = "Unknown or unsupported parameter format", m_cht = "未知的或未支援的參數格式" };
            ResultModel result = new ResultModel { Status = 422 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel PARAMETER_INVALID(object body,string field, string Lang)
        {
            var code = new ErrorCodeModel { r = 64, m = "Parameter "+field+" value is invalid", m_cht = "參數值"+field+"無效" };
            ResultModel result = new ResultModel { Status = 422 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel PARAMETER_CASH_OR_POINT(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 96, m = "Error trading, order must be paid all in cash or all in points", m_cht = "只能選擇現金或點數交易" };
            ResultModel result = new ResultModel { Status = 422 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel PARAMETER_CASH(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 160, m = "Parameter cash is not enough to pay the order", m_cht = "現金不足" };
            ResultModel result = new ResultModel { Status = 422 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel PARAMETER_NOT_ENOUGH_POINTS(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 1056, m = "User does not have enough points to pay the order", m_cht = "點數不足" };
            ResultModel result = new ResultModel { Status = 422 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel PARAMETER_ONLY_ONE_ALLOWED(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 2080, m = "Only receiver or sender can be defined, not both", m_cht = "參數from和to只能輸入一個" };
            ResultModel result = new ResultModel { Status = 422 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        // ============================================FILE============================================
        public static ResultModel FILE_NOT_UPLOADED(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 40, m = "File was not uploaded", m_cht = "檔案並未上傳" };
            ResultModel result = new ResultModel { Status = 422 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel FILE_TYPE_NOT_SUPPORTED(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 48, m = "File type not supported", m_cht = "檔案類型未支援" };
            ResultModel result = new ResultModel { Status = 422 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel FILE_TOO_BIG(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 288, m = "File size is too big", m_cht = "檔案太大" };
            ResultModel result = new ResultModel { Status = 422 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        // ============================================TOKEN============================================
        public static ResultModel TOKEN_ERROR_INVALID(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 65, m = "Token is invalid", m_cht = "Token無效" };
            ResultModel result = new ResultModel { Status = 401 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel TOKEN_ERROR_EXPIRED(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 66, m = "Token has expired", m_cht = "Token已過期" };
            ResultModel result = new ResultModel { Status = 401 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        // ============================================OS============================================
        public static ResultModel OS_MOVE_FILE(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 128, m = "Error moving file", m_cht = "移動檔案錯誤" };
            ResultModel result = new ResultModel { Status = 500 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        // ============================================AUTHENTICATION============================================
        public static ResultModel AUTHENTICATION_USERNAME_IN_USED(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 256, m = "Username already in used", m_cht = "使用者名稱已被使用" };
            ResultModel result = new ResultModel { Status = 422 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel AUTHENTICATION_ACCESS_DENIED(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 257, m = "Access denied", m_cht = "拒絕訪問" };
            ResultModel result = new ResultModel { Status = 401 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel AUTHENTICATION_ALREADY_LOGIN(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 258, m = "User has already login", m_cht = "使用者已登入" };
            ResultModel result = new ResultModel { Status = 401 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel AUTHENTICATION_ACCOUNT_PASSWORD_EXPIRED(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 260, m = "Account password has expired and need to be reset", m_cht = "密碼過期" };
            ResultModel result = new ResultModel { Status = 401 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel AUTHENTICATION_ACCOUNT_LOCKED(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 264, m = "Account lock for a period of time", m_cht = "登入錯誤, 鎖定中" };
            ResultModel result = new ResultModel { Status = 401 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        // ============================================SERVICE============================================
        public static ResultModel SERVICE_CONNECTION_ERROR(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 1024, m = "Error connecting to service", m_cht = "服務連結錯誤" };
            ResultModel result = new ResultModel { Status = 500 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel SERVICE_CA_ERROR(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 1025, m = "Certificate authority error", m_cht = "憑證授權錯誤" };
            ResultModel result = new ResultModel { Status = 500 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel SERVICE_FIREBASE(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 1026, m = "Error sending message through Google firebase cloud messaging service", m_cht = "Google firebase 雲訊息傳送失敗" };
            ResultModel result = new ResultModel { Status = 500 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        // ============================================UNKNOWN============================================
        public static ResultModel UNKNOWN_FUNCTION_NAME(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 2048, m = "Unknown function, this issue will be reported", m_cht = "未知的功能，此問題將會回報" };
            ResultModel result = new ResultModel { Status = 500 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        // ============================================BLOCKCHAIN============================================
        public static ResultModel BLOCKCHAIN_POINT_INITIALIZE(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 4096, m = "Error initializing user's point (asset)", m_cht = "初始化使用者點數失敗" };
            ResultModel result = new ResultModel { Status = 500 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel BLOCKCHAIN_POINT_INCREMENT(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 4097, m = "Error increasing admin points", m_cht = "新增管理員點數失敗" };
            ResultModel result = new ResultModel { Status = 500 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel BLOCKCHAIN_POINT_TRANSFER(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 4098, m = "Error transferring points", m_cht = "新增使用者點數失敗" };
            ResultModel result = new ResultModel { Status = 500 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel BLOCKCHAIN_CREATE_TRANSACTION_PROPOSAL(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 4100, m = "Error while creating transaction proposal", m_cht = "新增未簽名的交易提案失敗" };
            ResultModel result = new ResultModel { Status = 500 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel BLOCKCHAIN_COMMIT_TRANSACTION_PROPOSAL(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 4104, m = "Error while committing transaction proposal to the blockchain", m_cht = "提交簽名的交易提案失敗" };
            ResultModel result = new ResultModel { Status = 500 };

            result.Result = createArray(code, body, Lang);

            return result;
        }

        public static ResultModel BLOCKCHAIN_COMMIT_TRANSACTION(object body, string Lang)
        {
            var code = new ErrorCodeModel { r = 4112, m = "Error while committing the transaction to the blockchain", m_cht = "傳送簽名的交易失敗" };
            ResultModel result = new ResultModel { Status = 500 };

            result.Result = createArray(code, body, Lang);

            return result;
        }


        public static ResultModel CUSTOM_ERROR(object body,string msg,int status, string Lang)
        {
            var code = new ErrorCodeModel {  m = msg, m_cht = msg };
            ResultModel result = new ResultModel { Status = status };

            result.Result = createArray(code, body, Lang);

            return result;
        }

    }
}
