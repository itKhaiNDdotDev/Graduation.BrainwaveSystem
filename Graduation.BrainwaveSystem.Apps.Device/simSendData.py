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
            'deviceId': 'bcb6bd84-8247-4cce-acb4-48487b9015bb'
        },
        'delta': random.randint(0, 255),
        'theta': random.randint(100, 255),
        'alpha': random.randint(100, 255),
        'lowBeta': random.randint(0, 100),
        'midBeta': random.randint(0, 100),
        'highBeta': random.randint(0, 255),
        'gamma': random.randint(128, 255),
        'uhfGamma': random.randint(0, 255),
        'rawEEGs': [random.randint(150, 200) for _ in range(512)]
    }
    # processed_data = process_data(data)

    # Gửi dữ liệu lên server
    headers = {'Content-Type': 'application/json'}
    url = 'https://localhost:44321/api/DeviceDatas/bcb6bd84-8247-4cce-acb4-48487b9015bb'
    res = requests.post(url, data=json.dumps(data), headers=headers, verify=False)
    print(res)

    # Giải phóng bộ nhớ bằng gc.collect()
    del data, res, url
    gc.collect()

    # Đợi một khoảng thời gian trước khi lấy dữ liệu tiếp theo
    time.sleep(1)  # ví dụ: chờ 1 giây
