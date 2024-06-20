import matplotlib.pyplot as plt

m1 = 60  # Parasiutininko mase
m2 = 15  # Parasiuto mase (kartu 75kg)
k1 = 0.1  # Oro pasipriesinimas laisvo kritimo metu
k2 = 7    # Oro pasipriesinimas iskleidus parasiuta
h0 = 3500 # Aukstis is kurio sokama
tg = 25   # Sekundes po kuriu iskleidziamas parasiutas

parachute_deployed_height = 0
uzfiksuotasAukstis = False

def dvdt(t, v, k):
    g = 9.8
    return (((m1 + m2) * g) - (k * v * abs(v))) / (m1 + m2)

def simulate_parachute(dt):
    global uzfiksuotasAukstis
    global parachute_deployed_height
    v0 = 0
    v = v0
    h = h0
    t = 0

    t_list = []
    h_list = []
    v_list = []

    while h > 0:
        if t < tg:
            v += dt * dvdt(t, v, k1)
        else:
            if not uzfiksuotasAukstis:
                parachute_deployed_height = h
                uzfiksuotasAukstis = True
            v += dt * dvdt(t, v, k2)

        h -= dt * abs(v)
        t += dt

        t_list.append(t)
        h_list.append(h)
        v_list.append(v)

    return t_list, h_list, v_list

# Simulate parachute descent with different time steps
t_values_01, h_values_01, v_values_01 = simulate_parachute(0.1)
t_values_05, h_values_05, v_values_05 = simulate_parachute(0.12)
t_values_1, h_values_1, v_values_1 = simulate_parachute(0.14)
t_values_2, h_values_2, v_values_2 = simulate_parachute(0.16)
t_values_4, h_values_4, v_values_4 = simulate_parachute(0.18)
t_values_5, h_values_5, v_values_5 = simulate_parachute(0.22)
t_values_6, h_values_6, v_values_6 = simulate_parachute(0.24)
t_values_7, h_values_7, v_values_7 = simulate_parachute(0.25)
#t_values_20, h_values_20, v_values_20 = simulate_parachute(0.26)



# Plot all three graphs on one plot
plt.figure(figsize=(10, 8))
plt.plot(t_values_01, v_values_01, label='dt = 0.1 s')
plt.plot(t_values_05, v_values_05, label='dt = 0.12 s', linestyle='--')
plt.plot(t_values_1, v_values_1, label='dt = 0.14 s', linestyle=':')
plt.plot(t_values_2, v_values_2, label='dt = 0.16 s', linestyle='--')
plt.plot(t_values_4, v_values_4, label='dt = 0.18 s', linestyle=':')
plt.plot(t_values_5, v_values_5, label='dt = 0.22 s', linestyle=':')
plt.plot(t_values_6, v_values_6, label='dt = 0.24 s', linestyle=':')
plt.plot(t_values_7, v_values_7, label='dt = 0.25 s', linestyle=':')
#plt.plot(t_values_20, v_values_20, label='dt = 0.26 s', linestyle=':')
plt.xlabel('Laikas (s)')
plt.ylabel('Greitis (m/s)')
plt.title('Greičio ir laiko priklausomybė su skirtingais žingsniais')
plt.legend()
plt.grid(True)
plt.show()

touchdown_time = t_values_7[-1]
touchdown_speed = v_values_7[-1]

print(f"Parašiutininkas pasiekia žemę per: {touchdown_time:.2f} s")
print(f"Parašiutininko greitis kai pasieka žemę: {touchdown_speed:.2f} m/s")
print(f"Aukštis kuriame yra išskleidžiamas parašiutas: {parachute_deployed_height:.2f} m")
# Rest of the code remains the same for printing and displaying the results
