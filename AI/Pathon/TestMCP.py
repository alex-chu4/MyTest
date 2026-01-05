import os
from openai import OpenAI
from dotenv import load_dotenv


load_dotenv()
client = OpenAI(api_key=os.getenv("OPENAI_API_KEY"))

# 1. 定義 AI 代理人可以使用的工具 (Function)
def get_weather(location):
    """查詢指定地點的天氣"""
    return f"{location} 的天氣是晴天，氣溫 25 度。"

# 2. 定義工具的 JSON 描述，讓 ChatGPT 知道如何調用
tools = [
    {
        "type": "function",
        "function": {
            "name": "get_weather",
            "description": "獲取給定位置的天氣資訊",
            "parameters": {
                "type": "object",
                "properties": {
                    "location": {"type": "string", "description": "城市名稱，例如：台北"}
                },
                "required": ["location"],
            },
        },
    }
]

def run_ai_agent(user_prompt):
    print(f"--- AI代理人 接收任務: {user_prompt} ---")
    
    # 3. 初始調用 ChatGPT API應用
    messages = [{"role": "user", "content": user_prompt}]
    response = client.chat.completions.create(
        model="gpt-4-turbo-preview",
        messages=messages,
        tools=tools,
        tool_choice="auto"
    )
    
    response_message = response.choices[0].message
    tool_calls = response_message.tool_calls

    # 4. 判斷 AI Agent 是否決定調用工具
    if tool_calls:
        print("--- AI Agent 決定調用外部工具 ---")
        for tool_call in tool_calls:
            # 這裡實作具體的工具調用邏輯
            if tool_call.function.name == "get_weather":
                # 解析參數並執行函式
                import json
                args = json.loads(tool_call.function.arguments)
                result = get_weather(args['location'])
                print(f"工具回傳結果: {result}")
                
                # 將結果回傳給 AI 進行最後總結
                messages.append(response_message)
                messages.append({
                    "role": "tool",
                    "tool_call_id": tool_call.id,
                    "content": result
                })
        
        final_response = client.chat.completions.create(
            model="gpt-4-turbo-preview",
            messages=messages
        )
        return final_response.choices[0].message.content
    
    return response_message.content

# 執行 Python AI Agent實作 範例
if __name__ == "__main__":
    result = run_ai_agent("請問台北現在天氣如何？")
    print(f"AI代理人 最終回覆: {result}")