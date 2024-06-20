import matplotlib.pyplot as plt

m1 = 60  # parasiutininko mase
m2 = 15  # parasiuto mase (kartu 75kg)
k1 = 0.1  # oro pasipriesinimas laisvo kritimo metu
k2 = 7  # oro pasipriesinimas iskleidus parasiuta
h0 = 3500  # aukstis is kurio sokama
tg = 25  # sekundes po kuriu iskleidziamas parasiutas

parachute_deployed_height = 0
uzfiksuotasAukstis = False

def dvdt(t, v, k):
    g = 9.8
    return (((m1 + m2) * g) - (k * v * abs(v))) / (m1 + m2)

def rk4_method(dt):
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
            k1v = dt * dvdt(t, v, k1)
            k2v = dt * dvdt(t + dt/2, v + k1v/2, k1)
            k3v = dt * dvdt(t + dt/2, v + k2v/2, k1)
            k4v = dt * dvdt(t + dt, v + k3v, k1)
            v += (k1v + 2*k2v + 2*k3v + k4v) / 6
        else:
            if not uzfiksuotasAukstis:
                parachute_deployed_height = h
                uzfiksuotasAukstis = True
            k1v = dt * dvdt(t, v, k2)
            k2v = dt * dvdt(t + dt/2, v + k1v/2, k2)
            k3v = dt * dvdt(t + dt/2, v + k2v/2, k2)
            k4v = dt * dvdt(t + dt, v + k3v, k2)
            v += (k1v + 2*k2v + 2*k3v + k4v) / 6

        h -= dt * abs(v)
        t += dt

        t_list.append(t)
        h_list.append(h)
        v_list.append(v)

    return t_list, h_list, v_list

# Simulate parachute descent with different step sizes
t_values_01, h_values_01, v_values_01 = rk4_method(0.1)
t_values_02, h_values_02, v_values_02 = rk4_method(0.2)
t_values_03, h_values_03, v_values_03 = rk4_method(0.31)

# Plot all three graphs on one plot
plt.figure(figsize=(10, 8))
plt.plot(t_values_01, v_values_01, label='Step Size = 0.1 s')
plt.plot(t_values_02, v_values_02, label='Step Size = 0.2 s', linestyle='--')
plt.plot(t_values_03, v_values_03, label='Step Size = 0.3 s', linestyle=':')
plt.xlabel('Laikas (s)')
plt.ylabel('Greitis (m/s)')
plt.title('Greičio ir laiko priklausomybė (IV eilės Rungės ir Kutos metodas)')
plt.legend()
plt.grid(True)
plt.show()

touchdown_time = t_values_01[-1]
touchdown_speed = v_values_01[-1]

print(f"Parašiutininkas pasiekia žemę per: {touchdown_time:.2f} s")
print(f"Parašiutininko greitis kai pasieka žemę: {touchdown_speed:.2f} m/s")
print(f"Aukštis kuriame yra išskleidžiamas parašiutas: {parachute_deployed_height:.2f} m")
