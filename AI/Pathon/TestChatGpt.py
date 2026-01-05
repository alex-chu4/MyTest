import gradio as gr
from openai import OpenAI
import os

# 設置 OpenAI API 密鑰（請替換為您自己的 API 密鑰）
client = OpenAI(api_key=os.getenv("OPENAI_API_KEY", "sk-1234"))

# 儲存聊天記錄
conversation_history = []

# 聊天功能
def chat_with_gpt(user_input):
    global conversation_history
    
    if user_input:
        # 將用戶輸入添加到對話歷史
        conversation_history.append({"role": "user", "content": user_input})
        
        try:
            # 調用 OpenAI API
            response = client.chat.completions.create(
                #model="gpt-3.5-turbo",
                #model="gpt-5-nano",
                model="gpt-3.5-turbo",
                messages=conversation_history,
                max_completion_tokens=500,
                #temperature=0.7
            )
            
            # 獲取 AI 回應
            ai_response = response.choices[0].message.content
            
            # 將 AI 回應添加到對話歷史
            conversation_history.append({"role": "assistant", "content": ai_response})
            
            # 格式化顯示對話
            formatted_conversation = ""
            for msg in conversation_history:
                role = "You" if msg["role"] == "user" else "ChatGPT"
                formatted_conversation += f"**{role}:** {msg['content']}\n\n"
            
            return formatted_conversation
        
        except Exception as e:
            return f"Error: {str(e)}"
    
    return "Please enter a message."

# 清除對話歷史
def clear_conversation():
    global conversation_history
    conversation_history = []
    #return "Conversation cleared!"
    return ""

custom_css = """
#submit_button {
    background-color: red !important;
    color: white !important;
}
#clear_button {
    background-color: green !important;
    color: white !important;
}
"""

# Gradio 界面
with gr.Blocks(title="ChatGPT 聊天網頁",theme=gr.themes.Soft(), css=custom_css) as demo:
    gr.Markdown("# ChatGPT 聊天網頁")
    
    # 聊天顯示區域
    chatbot_output = gr.Textbox(
        label="對話歷史",
        placeholder="對話將顯示在這裡...",
        lines=10,
        max_lines=15
    )
    
    # 用戶輸入框
    # user_input = gr.Textbox(
    #     label="輸入您的訊息",
    #     placeholder="輸入您的問題或訊息..."
    # )
    user_input = gr.Textbox(
                label="輸入問題",
                placeholder="例如：檔案內容中提到什麼？",
                scale=3
    )
    gr.Examples(
                examples=[
                    "告訴我一個笑話",
                    "解釋量子力學 ",
                    "幫我寫詩 "                   
                ],
                inputs=user_input,
    )
    
    # 按鈕區域
    with gr.Row():
        submit_button = gr.Button("發送", elem_id="submit_button")         
        clear_button = gr.Button("清除對話", elem_id="clear_button")
    
    # 提示範例
    # with gr.Accordion("提示範例", open=True):
    #     gr.Button("告訴我一個笑話").click(
    #         fn=lambda: "Tell me a joke",
    #         outputs=user_input
    #     )
    #     gr.Button("解釋量子力學").click(
    #         fn=lambda: "Explain quantum mechanics in simple terms",
    #         outputs=user_input
    #     )
    #     gr.Button("幫我寫詩").click(
    #         fn=lambda: "Write a short poem about the stars",
    #         outputs=user_input
    #     )
    
    # 綁定按鈕事件
    submit_button.click(
        fn=chat_with_gpt,
        inputs=user_input,
        outputs=chatbot_output
    )
    
    clear_button.click(
        fn=clear_conversation,
        outputs=chatbot_output
    )

# 啟動 Gradio 界面
#demo.launch()
demo.launch(
        debug=False,
        server_name="127.0.0.1",
        server_port=8101,
        share=True
    )
