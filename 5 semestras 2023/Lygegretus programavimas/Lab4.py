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

def simulate_parachute():
    global uzfiksuotasAukstis
    global parachute_deployed_height
    v0 = 0
    v = v0
    dt = 0.1  # Žingsnis
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

t_values, h_values, v_values = simulate_parachute()

plt.figure(figsize=(8, 6))
plt.plot(t_values, v_values, label='Eulerio metodas')
plt.xlabel('Laikas (s)')
plt.ylabel('Greitis (m/s)')
plt.title('Greičio ir laiko priklausomybė (Eulerio metodas)')
plt.legend()
plt.grid(True)
plt.show()

touchdown_time = t_values[-1]
touchdown_speed = v_values[-1]

print(f"Parašiutininkas pasiekia žemę per: {touchdown_time:.2f} s")
print(f"Parašiutininko greitis kai pasieka žemę: {touchdown_speed:.2f} m/s")
print(f"Aukštis kuriame yra išskleidžiamas parašiutas: {parachute_deployed_height:.2f} m")