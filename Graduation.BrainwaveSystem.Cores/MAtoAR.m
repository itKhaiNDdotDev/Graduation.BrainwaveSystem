clc
if ~isempty(instrfind)
    fclose(instrfind);
    delete(instrfind);
end

% Open the serial port
s = serial("COM4", "BaudRate", 57600);
fopen(s); 

% Initialize variables
x = -1;
y = [];

% Plot the data in real-time
while x < 50000
  x = x + 1;
  data = fscanf(s);
  y = [y, str2double(data)];
  drawnow;
end

% Remove NaN
y(isnan(y)) = [];
figure(1);
plot(y)

flm = 500;            % Sampling frequency                    
T = 1/flm;             % Sampling period       
L = length(y);             % Length of signal
t = (0:L-1)*T;        % Time vector

% filter 8 35
    % [B A]=butter(3,[16/(flm) 80/(flm)]);
    % y = filter(B,A,y);
% FFT
f = flm*(0:(L/2))/L;
Y = fft(y);
Y(1)=0;
P2 = abs(Y/L);
P1 = P2(1:(L/2+1));
P1(2:end-1) = 2*P1(2:end-1);
figure(2);
plot(f,P1) 
title("Phổ tần số sóng não chuẩn")
xlabel("f (Hz)")
ylabel("|P1(f)|")

% Close the serial port
fclose(s);
