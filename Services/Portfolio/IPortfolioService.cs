using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Dtos;


namespace PortfolioServices;

public interface IPortfolioService
{
    List<ProjectDto> GetAllProjects();
}