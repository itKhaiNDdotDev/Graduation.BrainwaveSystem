import serial
import numpy as np
import matplotlib.pyplot as plt
import pandas as pd
# Close any existing open serial ports

if serial.Serial:
    serial.Serial().close()

# Open the serial port
s = serial.Serial("COM5", baudrate=57600)


def FeatureExtract(y):
    flm = 512
    L = len(y) #512*15
    Y = np.fft.fft(y) #Giá trị output FFT: giá trị phổ tương ứng x tần suất là tần số (Y có thể âm)
    Y[0] = 0        
    P2 = np.abs(Y / L) #1 dãy giá trị tần số (chuẩn hóa - ko âm)
    P1 = P2[:L // 2 + 1] #Đi search và hỏi chatGPT dẻ clear 2 dòng
    P1[1:-1] = 2 * P1[1:-1]
    
    # => Đồ thị FFT: chiều x: biến đếm, chiều y: giá trị của P1
    ## Plot lên đồ thị: plot

    # Find the indices of the frequency values between 0.5 Hz and 4 Hz
    f1 = np.arange(len(P1)) * flm / len(P1)
    indices1 = np.where((f1 >= 0.5) & (f1 <= 4))[0]
    delta = np.sum(P1[indices1])

    f1 = np.arange(len(P1)) * flm / len(P1)
    indices1 = np.where((f1 >= 4) & (f1 <= 8))[0]
    theta = np.sum(P1[indices1])

    f1 = np.arange(len(P1)) * flm / len(P1)
    indices1 = np.where((f1 >= 8) & (f1 <= 13))[0]
    alpha = np.sum(P1[indices1])

    f1 = np.arange(len(P1)) * flm / len(P1)
    indices1 = np.where((f1 >= 13) & (f1 <= 30))[0]
    beta = np.sum(P1[indices1])

    abr = alpha / beta
    tbr = theta / beta
    dbr = delta / beta
    tar = theta / alpha
    dar = delta / alpha
    dtabr = (alpha + beta) / (delta + theta)
    dict = {"delta": delta,
            "theta": theta,
            "alpha": alpha,
            "beta": beta,
            "abr": abr,
            "tbr": tbr,
            "dbr": dbr,
            "tar": tar,
            "dar": dar,
            "dtabr": dtabr
            }
    #print(dict)
    return dict


x = 0
y = []
z = []
feature = []

while x < (180 * 512):
    x += 1
    data = s.readline().decode('ascii').strip()  # strip removes leading/trailing whitespace
    try:
        value = float(data)
    except ValueError:
        continue  # skip this line if it can't be converted to a float
    if (value > -256) and (value < 256):
        y.append(value)

        with open("raw.txt", 'a') as f:
            f.writelines(value)

    # k = 1 * 512
    # if (x % k == 0):
    #     print(x/512)
    #     y = np.array(y)
    #     y = y[~np.isnan(y)]
    #     feature.append(FeatureExtract(y))
    #     print(FeatureExtract(y))
    #     df = pd.DataFrame.from_dict(feature)
    #     y = []
    plt.pause(0.0001)
#df.to_csv("Normaliso2.csv")
# Close the serial port
s.close()
