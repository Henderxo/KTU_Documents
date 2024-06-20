import matplotlib.pyplot as plt
import numpy as np
from scipy.integrate import odeint

m1 = 60  # parasiutininko mase
m2 = 15  # parasiuto mase (kartu 75kg)
k1 = 0.1  # oro pasipriesinimas laisvo kritimo metu
k2 = 7  # oro pasipriesinimas iskleidus parasiuta
h0 = 3500  # aukstis is kurio sokama
tg = 25  # sekundes po kuriu iskleidziamas parasiutas

def dvdt(v, t, k):
    g = 9.8
    return (((m1 + m2) * g) - (k * v * abs(v))) / (m1 + m2)

def model(y, t, k):
    v = y[0]
    h = y[1]
    dydt = [dvdt(v, t, k), -abs(v)]
    return dydt

# Initial conditions
y0 = [0, h0]

# Time points for the first stage
t1 = np.arange(0, tg, 0.1)

# Solve ODE using odeint for the first stage
result1 = odeint(model, y0, t1, args=(k1,))
v_values_eulerio = result1[:, 0]

# Extract the final height and velocity from the first stage
final_height = result1[-1, 1]
final_velocity = result1[-1, 0]

# Set initial conditions for the second stage
y0 = [final_velocity, final_height]

# Time points for the second stage
t2 = np.arange(tg, 205, 0.1)

# Continue solving ODE using odeint with k2 for the second stage
result2 = odeint(model, y0, t2, args=(k2,))
v_values_parachute = result2[:, 0]

# Concatenate the results from both stages
t = np.concatenate((t1, t2))
v_values = np.concatenate((v_values_eulerio, v_values_parachute))

# Plot the results
plt.figure(figsize=(10, 6))
plt.plot(t, v_values, label='Greitis')
plt.xlabel('Laikas (s)')
plt.ylabel('Greitis (m/s)')
plt.title('Greičio ir laiko priklausomybė (SciPy odeint)')
plt.legend()
plt.grid(True)
plt.show()

# Extract values for printing
touchdown_time = t[-1]
touchdown_speed = v_values[-1]
parachute_deployed_height = final_height

# Print the values
print(f"Parašiutininkas pasiekia žemę per: {touchdown_time:.2f} s")
print(f"Parašiutininko greitis kai pasieka žemę: {touchdown_speed:.2f} m/s")
print(f"Aukštis kuriame yra išskleidžiamas parašiutas: {parachute_deployed_height:.2f} m")
