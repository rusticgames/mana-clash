joystick_count = 11
axis_count = 28
for j in range(1,joystick_count + 1):
	for a in range(0,axis_count):
		print("  - serializedVersion: 3")
		print("    m_Name: j{0}_{1}".format(j,a))
		print("    descriptiveName:")
		print("    descriptiveNegativeName:")
		print("    negativeButton:")
		print("    positiveButton:")
		print("    altNegativeButton:")
		print("    altPositiveButton:")
		print("    gravity: 0")
		print("    dead: 0.19")
		print("    sensitivity: 1")
		print("    snap: 0")
		print("    invert: 0")
		print("    type: 2")
		print("    axis: {0}".format(a))
		print("    joyNum: {0}".format(j))
