import gradio as gr
import google.generativeai as genai
from PIL import Image
import os
import numpy
import gradio_client.utils as _gc_utils
 
_orig_json_schema_to_python_type = getattr(_gc_utils, "json_schema_to_python_type", None)
def _safe_json_schema_to_python_type(schema, *args, **kwargs):
    try:
        if _orig_json_schema_to_python_type is None:
            return schema
        if not isinstance(schema, (dict, list)) and schema is not None:
            return "Any"
        return _orig_json_schema_to_python_type(schema, *args, **kwargs)
    except (TypeError, AttributeError):
        return "Any"

_gc_utils.json_schema_to_python_type = _safe_json_schema_to_python_type

API_KEY = os.environ['GOOGLE_API_KEY']
genai.configure(api_key=API_KEY)

# 初始化 Gemini 模型（支援圖片輸入的模型）
try:
    model = genai.GenerativeModel('gemini-2.5-pro')  # 使用正確的模型名稱
except Exception as e:
    print(f"初始化 Gemini 模型時發生錯誤: {e}")

def perform_ocr(image):
    """
    使用 Gemini 進行 OCR：將圖片轉為 base64 並發送提示提取文字。
    """
    try:
        if image is None:
            return "請上傳圖片檔案。"
        
        # 將圖片轉為 PIL Image（Gradio 已自動處理）
        if isinstance(image, str):
            # 如果收到檔案路徑
            pil_image = Image.open(image)
        elif isinstance(image, numpy.ndarray):
            # 如果收到 numpy array
            pil_image = Image.fromarray(image)
        else:
            return "不支援的圖片格式。請上傳圖片檔案或提供圖片路徑。"
        
        # 準備提示：要求模型提取圖片中的所有文字
        prompt = "請從這張圖片中提取所有可見的文字，並以結構化的格式輸出（例如清單或段落）。如果有表格，請保持格式。"
        
        # 生成內容（傳入圖片和提示）
        response = model.generate_content([pil_image, prompt])
        
        # 回傳提取的文字
        return response.text if response.text else "無法從圖片中提取文字。"
    
    except Exception as e:
        return f"處理圖片時發生錯誤: {str(e)}"

# 建立最簡化版的 Gradio 介面
demo = gr.Interface(
    fn=perform_ocr,
    inputs=gr.Image(type="numpy", label="上傳圖片"),
    outputs=gr.Textbox(label="提取的文字", lines=8),
    title="AI OCR 工具",
    description="上傳圖片來提取文字",
    examples=None,
    cache_examples=False
)

# 啟動 Gradio（自動開啟瀏覽器）
if __name__ == "__main__":
    demo.launch(
        debug=False,
        server_name="127.0.0.1",
        server_port=8110,
        share=True  # 移除固定端口，讓 Gradio 自動選擇可用端口
    )