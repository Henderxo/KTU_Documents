import matplotlib.pyplot as plt
import numpy as np
from scipy.integrate import solve_ivp

m1 = 60  # parasiutininko mase
m2 = 15  # parasiuto mase (kartu 75kg)
k1 = 0.1  # oro pasipriesinimas laisvo kritimo metu
k2 = 7  # oro pasipriesinimas iskleidus parasiuta
h0 = 3500  # aukstis is kurio sokama
tg = 25  # sekundes po kuriu iskleidziamas parasiutas

def dvdt(t, y, k):
    v, h = y
    g = 9.8
    dydt = [(((m1 + m2) * g) - (k * v * abs(v))) / (m1 + m2), -abs(v)]
    return dydt

# Initial conditions
y0 = [0, h0]

# Time span
t_span1 = (0, tg)
t_span2 = (tg, 300)

# Solve ODE using solve_ivp for the first stage
sol1 = solve_ivp(dvdt, t_span1, y0, args=(k1,), dense_output=True)

# Extract the final height and velocity from the first stage
final_height = sol1.sol(tg).y[1][-1]
final_velocity = sol1.sol(tg).y[0][-1]

# Set initial conditions for the second stage
y0 = [final_velocity, final_height]

# Solve ODE using solve_ivp for the second stage
sol2 = solve_ivp(dvdt, t_span2, y0, args=(k2,), dense_output=True)

# Concatenate the results from both stages
t_values = np.concatenate((sol1.t, sol2.t))
v_values = np.concatenate((sol1.sol(t_values)[0], sol2.sol(t_values)[0]))

# Plot the results
plt.figure(figsize=(10, 6))
plt.plot(t_values, v_values, label='Greitis')
plt.xlabel('Laikas (s)')
plt.ylabel('Greitis (m/s)')
plt.title('Greičio ir laiko priklausomybė (SciPy solve_ivp)')
plt.legend()
plt.grid(True)
plt.show()

# Extract values for printing
touchdown_time = t_values[-1]
touchdown_speed = v_values[-1]
parachute_deployed_height = final_height

# Print the values
print(f"Parašiutininkas pasiekia žemę per: {touchdown_time:.2f} s")
print(f"Parašiutininko greitis kai pasieka žemę: {touchdown_speed:.2f} m/s")
print(f"Aukštis kuriame yra išskleidžiamas parašiutas: {parachute_deployed_height:.2f} m")
