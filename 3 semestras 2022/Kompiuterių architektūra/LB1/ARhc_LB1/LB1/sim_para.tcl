lappend auto_path "C:/lscc/diamond/3.12/data/script"
package require simulation_generation
set ::bali::simulation::Para(DEVICEFAMILYNAME) {LatticeXP2}
set ::bali::simulation::Para(PROJECT) {LB1}
set ::bali::simulation::Para(PROJECTPATH) {C:/Users/Lenovo/Desktop/Architechtura/LB1/ARhc_LB1}
set ::bali::simulation::Para(FILELIST) {"C:/Users/Lenovo/Desktop/Architechtura/LB1/N_flg.vhd" "C:/Users/Lenovo/Desktop/Architechtura/LB1/N_reg.vhd" "C:/Users/Lenovo/Desktop/Architechtura/LB1/N_cnt.vhd" "C:/Users/Lenovo/Desktop/Architechtura/LB1/N_rom.vhd" "C:/Users/Lenovo/Desktop/Architechtura/LB1/N_alu_s.vhd" "C:/Users/Lenovo/Desktop/Architechtura/LB1/N_mux.vhd" "C:/Users/Lenovo/Desktop/Architechtura/LB1/N_ctrl.vhd" "C:/Users/Lenovo/Desktop/Architechtura/LB1/N_top.vhd" }
set ::bali::simulation::Para(GLBINCLIST) {}
set ::bali::simulation::Para(INCLIST) {"none" "none" "none" "none" "none" "none" "none" "none"}
set ::bali::simulation::Para(WORKLIBLIST) {"work" "work" "work" "work" "work" "work" "work" "work" }
set ::bali::simulation::Para(COMPLIST) {"VHDL" "VHDL" "VHDL" "VHDL" "VHDL" "VHDL" "VHDL" "VHDL" }
set ::bali::simulation::Para(SIMLIBLIST) {pmi_work ovi_xp2}
set ::bali::simulation::Para(MACROLIST) {}
set ::bali::simulation::Para(SIMULATIONTOPMODULE) {TOP}
set ::bali::simulation::Para(SIMULATIONINSTANCE) {}
set ::bali::simulation::Para(LANGUAGE) {VHDL}
set ::bali::simulation::Para(SDFPATH)  {}
set ::bali::simulation::Para(INSTALLATIONPATH) {C:/lscc/diamond/3.12}
set ::bali::simulation::Para(ADDTOPLEVELSIGNALSTOWAVEFORM)  {1}
set ::bali::simulation::Para(RUNSIMULATION)  {1}
set ::bali::simulation::Para(HDLPARAMETERS) {}
set ::bali::simulation::Para(POJO2LIBREFRESH)    {}
set ::bali::simulation::Para(POJO2MODELSIMLIB)   {}
::bali::simulation::ModelSim_Run
