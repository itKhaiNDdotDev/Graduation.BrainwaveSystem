import requests
import json
import gc
import time
import random

while True:
    # Lấy dữ liệu từ thiết bị IoT và xử lý
    data = {
        'general': {
            'poorQuality': random.randint(0, 100),
            'attention': random.randint(128, 255),
            'meditation': random.randint(64, 196),
            'deviceId': '554b7638-80f9-460d-f8b5-08db94f94fe3',
            "createdBy": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "lastModifiedBy": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        },
        'delta': random.randint(0, 50),
        'theta': random.randint(0, 100),
        'alpha': random.randint(50, 100),
        'lowBeta': random.randint(255, 260),
        'midBeta': random.randint(100, 200),
        'highBeta': random.randint(200, 255),
        'gamma': random.randint(128, 255),
        'uhfGamma': random.randint(0, 255),
        'rawEEGs': [random.randint(-255, 255) for _ in range(512)],
        "createdBy": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "lastModifiedBy": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    }
    # processed_data = process_data(data)

    # Gửi dữ liệu lên server
    headers = {'Content-Type': 'application/json', 'Authorization': 'Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiS2hhaU5EVXNlciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6InVzZXIiLCJuYW1lIjoiTmd1eeG7hW4gRHV5IEtoYWkiLCJ1c2VyTmFtZSI6IktoYWlORFVzZXIiLCJ1c2VySWQiOiIwYTQ4ZGI5NS1kNzRjLTQ3N2UtNmExYy0wOGRiOTRmNjlmNDUiLCJyb2xlIjoidXNlciIsImV4cCI6MTcyMjg4NDI2NywiaXNzIjoiQ2xpZW50IiwiYXVkIjoiU2VydmVyIn0.rChaYZ9Bb0gk9Rxh6LQCLhv3GKvMBxD4OoWIeLACTxM'}
    url = 'https://localhost:44321/api/DeviceDatas/554b7638-80f9-460d-f8b5-08db94f94fe3'
    res = requests.post(url, data=json.dumps(data), headers=headers, verify=False)
    print(res)

    # Giải phóng bộ nhớ bằng gc.collect()
    del data, res, url
    gc.collect()

    # Đợi một khoảng thời gian trước khi lấy dữ liệu tiếp theo
    time.sleep(1)  # ví dụ: chờ 1 giây
